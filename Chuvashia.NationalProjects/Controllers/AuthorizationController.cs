using Chuvashia.NationalProjects.Binding;
using Chuvashia.NationalProjects.Context;
using Chuvashia.NationalProjects.Model;
using Chuvashia.NationalProjects.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chuvashia.NationalProjects.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private NationalProjectsDbContext _context;
        public AuthorizationController(NationalProjectsDbContext context)
        {
            _context = context;
        }

        private AuthorizationAdminView GetToken(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, admin.Role.ToString())
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new AuthorizationAdminView
            {
                Bearer = encodedJwt,
                Admin = admin
            };
            return response;
        }

        /// <summary>
        /// Authorization by login and password
        /// </summary>
        [Authorize(AuthenticationSchemes = "admin")]
        [ProducesResponseType(typeof(Admin), 200)]
        [HttpPost("admin/login")]
        public async Task<ActionResult<Admin>> Login([FromBody]AuthorizationBinding binding)
        {
            Admin admin = await _context.Admins.Where(o => o.Login == binding.Login && o.Password == binding.Password).FirstOrDefaultAsync();
            if (admin != null)
            {
                return Ok(GetToken(admin));
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Create a new test administrator
        /// </summary>
        [Authorize(AuthenticationSchemes = "admin")]
        [ProducesResponseType(typeof(Admin), 200)]
        [HttpPost("admin/test")]
        public async Task<ActionResult<Admin>> CreateTestAdmin()
        {
            var count = await _context.Admins.CountAsync();
            if(count > 0)
            {
                return BadRequest("Admin exists");
            }

            var admin = new Admin()
            {
                Id = Guid.NewGuid(),
                FirstName = "Сазонов",
                MiddleName = "Иван",
                LastName = "Васильевич",
                Login = "admin",
                Password = "admin",
                Phone = "+79225401010",
                Role = AdminRole.Admin
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok(admin);
        }
    }
}