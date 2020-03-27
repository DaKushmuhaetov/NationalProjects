using Chuvashia.NationalProjects.Context;
using Chuvashia.NationalProjects.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Chuvashia.NationalProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private NationalProjectsDbContext _context;
        public AuthorizationController(NationalProjectsDbContext context)
        {
            _context = context;
        }



        private GetToken(Admin admin)
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

            var response = new
            {
                access_token = encodedJwt,
                Login = claimsIdentity.Name
            };
            return Json(response);
        }
        //Get :api/Autorization
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAtorization(string Login,string Password)
        {
            Admin admin = await(_context.Admins.Where(o => o.Login == Login && o.Password == Password).FirstOrDefaultAsync());
            if (admin != null)
            {
                return()
            }
        } 
    }
}