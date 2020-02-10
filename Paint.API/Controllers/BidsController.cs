using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paint.Data;
using Paint.Domain;
using Paint.DTO;
using RandREng.Paging;

namespace Paint.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IPaintRepository _repository;

        public BidsController(IPaintRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Bid
        [HttpGet]
        public async Task<ActionResult<PagedResult<BidListItem>>> GetBidSheets(
            [FromQuery] int page, 
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
            return await _repository.GetBidListAsync<BidListItem>(page, pageSize, sortColumn, sortDirection);
        }

        // GET: api/Bid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBidSheet(int id)
        {
            var data = await _repository.GetBidSheetAsync<Bid>(id);
            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        [HttpGet("item/{id}")]
        public async Task<ActionResult<BidItemDto>> GetBidItem(int id)
        {
            var data = await _repository.GetBidItem<BidItemDto>(id);
            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        /*
        // PUT: api/Bid/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidSheet(int id, BidSheet bidSheet)
        {
            if (id != bidSheet.Id)
            {
                return BadRequest();
            }

            _repository.Entry(bidSheet).State = EntityState.Modified;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidSheetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bid
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BidSheet>> PostBidSheet(BidSheet bidSheet)
        {
            _repository.BidSheets.Add(bidSheet);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("GetBidSheet", new { id = bidSheet.Id }, bidSheet);
        }

        // DELETE: api/Bid/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BidSheet>> DeleteBidSheet(int id)
        {
            var bidSheet = await _repository.BidSheets.FindAsync(id);
            if (bidSheet == null)
            {
                return NotFound();
            }

            _repository.BidSheets.Remove(bidSheet);
            await _repository.SaveChangesAsync();

            return bidSheet;
        }

        private bool BidSheetExists(int id)
        {
            return _repository.BidSheets.Any(e => e.Id == id);
        }

    */
    }
}
