using Chuvashia.NationalProjects.Binding;
using Chuvashia.NationalProjects.Context;
using Chuvashia.NationalProjects.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        [ProducesResponseType(typeof(Counter), 201)]
        public async Task<ActionResult<Counter>> AddCounter([FromBody] CounterBinding binding,
            CancellationToken cancellationToken)
        {
            var dbCounter = await _context.Counters
                .AsNoTracking()
                .Where(o => o.Type == binding.Type)
                .SingleOrDefaultAsync();

            if (dbCounter != null)
            {
                return BadRequest($"Counter with Type: {binding.Type} already created");
            }

            var counter = new Counter()
            {
                Id = Guid.NewGuid(),
                Type = binding.Type,
                Amount = binding.Amount
            };


            _context.Counters.Add(counter);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(counter);
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

        /// <summary>
        /// Update counter
        /// </summary>
        [HttpPut("counter/{id}/{amount}")]
        public async Task<ActionResult<Counter>> UpdateCounter([FromRoute]Guid id,
            [FromRoute]decimal amount,
            CancellationToken cancellationToken)
        {
            if (amount < 0)
                return ValidationProblem($"Amount must be greater or equal than 0. Amount was: {amount}");

            var counter = await _context.Counters.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (counter == null)
                return NotFound();

            counter.Amount = amount;
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(counter);
        }
    }
}