using Microsoft.AspNetCore.Mvc;
using Paint.Data;
using Paint.Domain;
using RandREng.Paging.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paint.MVC.ViewComponents
{
    public class JobListComponent : ViewComponent
    {
        private readonly Context _context;

        public JobListComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId, int pageNumber)
        {
            return View(await _context.Jobs.Where(j => j.ClientId == clientId).ToPageResultAsync<Job>(1, 20));
        }

    }
}
