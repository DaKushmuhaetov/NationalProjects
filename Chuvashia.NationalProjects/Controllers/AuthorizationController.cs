using Chuvashia.NationalProjects.Context;
using Microsoft.AspNetCore.Mvc;

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
    }
}