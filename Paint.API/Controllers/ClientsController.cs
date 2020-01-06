using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paint.Data;
using Paint.Domain;
using Paint.DTO;
using RandREng.Paging;
using RandREng.Paging.EFCore;

namespace Paint.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IPaintRepository _paintRepository;

        public ClientsController(Context context, IMapper mapper, IPaintRepository paintRepository)
        {
            _context = context;
            _mapper = mapper;
            this._paintRepository = paintRepository;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientItem>>> GetClients()
        {
            return _mapper.Map<List<Client>, List<ClientItem>> (await _context.Clients.Include(c => c.ClientType).ToListAsync());
        }

        [HttpGet("{id}/jobs")]
        public async Task<ActionResult<PagedResult<JobItem2>>> GetClientJobs(
            int id, 
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
            return await this._paintRepository.GetJobListAsync<JobItem2>(page, pageSize, sortColumn, sortDirection, id);
        }

        [HttpGet("{id}/bids")]
        public async Task<ActionResult<PagedResult<BidListItem2>>> GetClientBids(
            int id, 
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
            return await this._paintRepository.GetBidListAsync<BidListItem2>(page, pageSize, sortColumn, sortDirection, id);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDetails>> GetClient(int id)
        {
            ClientDetails client = _mapper.Map<Client, ClientDetails>( 
                await _context.Clients
                    .Include(c => c.Clients).ThenInclude(ct => ct.ClientType)
                    .Include(c => c.ClientType)
                    .FirstOrDefaultAsync(c => c.Id == id));

            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
