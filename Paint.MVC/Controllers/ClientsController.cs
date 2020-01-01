using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Paint.Data;
using Paint.Domain;
using Paint.MVC.Helpers;
using Paint.MVC.Models;
using RandREng.Paging.EFCore;

namespace Paint.MVC.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly Context _context;

        public ClientsController(Context context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var context = _context.Clients.Include(c => c.ClientType).Include(c => c.Parent);
            return View(await context.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.ClientType)
                .Include(c => c.Parent)
                .Include(c => c.Clients)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            var model = new ClientViewModel();
            model.Active = client.Active;
            model.BillingAddress = client.BillingAddress?.GetFormattedSiteAddress();
            model.Clients = client.Clients;
            model.ClientType = client.ClientType.Name;
            model.Notes = client.Notes;
            model.Name = client.Name;

            model.Jobs = await _context.Jobs.Where(j => j.ClientId == client.Id).ToPageResultAsync<Job>(1, 20);
//            model.Jobs.Results[0].Address.GetFormattedSiteAddress();
//            model.Jobs.Results[0].Address.Line1;

            return View(model);
        }

        // GET: Clients/Create
        public IActionResult Create(int? id)
        {
            Client client = new Client();
            client.ParentId = id;
            ViewData["ClientTypeId"] = new SelectList(_context.Set<ClientType>(), "Id", "Name");
            ViewData["ParentId"] = NullableSelectList.Create<int,Client>(_context.Set<Client>(), "Id", "Name", "-- none --") ;
            return View(client);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,ClientTypeId,FirstName,LastName,CompanyName,Notes,Active")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientTypeId"] = new SelectList(_context.Set<ClientType>(), "Id", "Name", client.ClientTypeId);
            //            ViewData["ParentId"] = new SelectList(_context.Clients, "Id", "Name", client.ParentId);
            ViewData["ParentId"] = NullableSelectList.Create<int, Client>(_context.Set<Client>(), "Id", "Name", "-- none --");
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["ClientTypeId"] = new SelectList(_context.Set<ClientType>(), "Id", "Name", client.ClientTypeId);
            ViewData["ParentId"] = NullableSelectList.Create<int, Client>(_context.Set<Client>(), "Id", "Name", "-- none --");
//            ViewData["ParentId"] = new SelectList(_context.Clients, "Id", "Name", client.ParentId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,ClientTypeId,FirstName,LastName,CompanyName,Notes,Active")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientTypeId"] = new SelectList(_context.Set<ClientType>(), "Id", "Name", client.ClientTypeId);
            //            ViewData["ParentId"] = new SelectList(_context.Clients, "Id", "Name", client.ParentId);
            ViewData["ParentId"] = NullableSelectList.Create<int, Client>(_context.Set<Client>(), "Id", "Name", "-- none --");
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.ClientType)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
