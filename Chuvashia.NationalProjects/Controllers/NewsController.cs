using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chuvashia.NationalProjects.Binding;
using Chuvashia.NationalProjects.Context;
using Chuvashia.NationalProjects.Model.News;
using Chuvashia.NationalProjects.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chuvashia.NationalProjects.Controllers
{
    [Route("api")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private NationalProjectsDbContext _context;
        public NewsController(NationalProjectsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of news
        /// </summary>
        [HttpGet("news")]
        [ProducesResponseType(typeof(Page<NewsPost>), 200)]
        public async Task<ActionResult<Page<NewsPost>>> GetNewsPosts(
            [FromQuery]GetNewsListBinding binding,
            CancellationToken cancellationToken)
        {
            return await (new NewsQueryHandler(_context).GetPage(binding));
        }
    }
}