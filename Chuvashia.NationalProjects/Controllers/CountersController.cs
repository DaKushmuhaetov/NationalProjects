using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chuvashia.NationalProjects.Context;
using Chuvashia.NationalProjects.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chuvashia.NationalProjects.Controllers
{
    [Route("api")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        private NationalProjectsDbContext _context;
        public CountersController(NationalProjectsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of counters
        /// </summary>
        /// <response code="200"></returns>
        [HttpGet("counters")]
        [ProducesResponseType(typeof(List<Counter>), 200)]
        public async Task<ActionResult<List<Counter>>> GetCounters(
            CancellationToken cancellationToken)
        {
            return await _context.Counters.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Create new counter
        /// </summary>
        /// <param name="counter">New counter</param>
        [HttpPost("counters")]
        public async Task<IActionResult> AddCounter([FromBody] Counter counter,
            CancellationToken cancellationToken)
        {
            var dbCounter = await _context.Counters.Where(o => o.Type == counter.Type).SingleOrDefaultAsync();
            if (dbCounter != null)
            {
                return BadRequest($"Counter with Type: {counter.Type} already created");
            }

            _context.Counters.Add(counter);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Remove counter
        /// </summary>
        /// <param name="id">Counter id</param>
        [HttpDelete("counters/{id}")]
        public async Task<IActionResult> RemoveCounter([FromRoute]Guid id,
            CancellationToken cancellationToken)
        {
            var counter = await _context.Counters.Where(o => o.Id == id).FirstOrDefaultAsync();
            _context.Counters.Remove(counter);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}