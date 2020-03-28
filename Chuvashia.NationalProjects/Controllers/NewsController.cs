using Chuvashia.NationalProjects.Binding;
using Chuvashia.NationalProjects.Context;
using Chuvashia.NationalProjects.Model.News;
using Chuvashia.NationalProjects.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// Get news post by id
        /// </summary>
        [HttpGet("news/{id}")]
        [ProducesResponseType(typeof(NewsPost), 200)]
        public async Task<ActionResult<NewsPost>> GetNewsPost(
            [FromRoute]Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.NewsPosts.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Create a news post at selected national project
        /// </summary>
        [HttpPost("news")]
        [ProducesResponseType(typeof(NewsPost), 200)]
        public async Task<ActionResult<NewsPost>> CreateNewsPost(
            [FromBody]CreateNewsBinding binding,
            CancellationToken cancellationToken)
        {
            var newsPost = new NewsPost()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Title = binding.Title,
                Text = binding.Text,
                Type = binding.Type
            };

            _context.NewsPosts.Add(newsPost);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(newsPost);
        }

        /// <summary>
        /// Update a news post
        /// </summary>
        /// <param name="id">News post id</param>
        [HttpPut("news/{id}")]
        [ProducesResponseType(typeof(NewsPost), 200)]
        public async Task<ActionResult<NewsPost>> UpdatePost(
            [FromRoute] Guid id,
            [FromBody] CreateNewsBinding binding,
            CancellationToken cancellationToken)
        {
            var newsPost = await _context.NewsPosts.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (newsPost == null)
                return NotFound();

            newsPost.Type = binding.Type;
            newsPost.Title = binding.Title;
            newsPost.Text = binding.Text;

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(newsPost);
        }

        /// <summary>
        /// Archive a news post
        /// </summary>
        /// <param name="id">News post id</param>
        [HttpPut("news/archive/{id}")]
        public async Task<IActionResult> ArchivePost(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var newsPost = await _context.NewsPosts.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (newsPost == null)
                return NotFound();

            newsPost.IsArchived = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Unarchive a news post
        /// </summary>
        /// <param name="id">News post id</param>
        [HttpPut("news/unarchive/{id}")]
        public async Task<ActionResult<NewsPost>> UnArchivePost(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var newsPost = await _context.NewsPosts.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (newsPost == null)
                return NotFound();

            newsPost.IsArchived = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(newsPost);
        }
    }
}