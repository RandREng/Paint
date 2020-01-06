using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paint.Data;
using Paint.Domain;
using Paint.DTO;
using RandREng.Paging;

namespace Paint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IPaintRepository _repository;

        public JobsController(IPaintRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<PagedResult<JobItem>>> GetJobs(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 25,
            [FromQuery] string sortColumn = null,
            [FromQuery] string sortDirection = null)
        {
            if (page < 1)
            {
                page = 1;
            }
            if (pageSize < 1 || pageSize > 50)
            {
                pageSize = 50;
            }
            return await _repository.GetJobListAsync<JobItem>(page, pageSize, sortColumn, sortDirection, null);
        }

        //// GET: api/Jobs/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Job>> GetJob(int id)
        //{
        //    var job = await _context.Jobs.FindAsync(id);

        //    if (job == null)
        //    {
        //        return NotFound();
        //    }

        //    return job;
        //}

        //// PUT: api/Jobs/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutJob(int id, Job job)
        //{
        //    if (id != job.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(job).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!JobExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Jobs
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Job>> PostJob(Job job)
        //{
        //    _context.Jobs.Add(job);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetJob", new { id = job.Id }, job);
        //}

        //// DELETE: api/Jobs/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Job>> DeleteJob(int id)
        //{
        //    var job = await _context.Jobs.FindAsync(id);
        //    if (job == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Jobs.Remove(job);
        //    await _context.SaveChangesAsync();

        //    return job;
        //}

        //private bool JobExists(int id)
        //{
        //    return _context.Jobs.Any(e => e.Id == id);
        //}
    }
}
