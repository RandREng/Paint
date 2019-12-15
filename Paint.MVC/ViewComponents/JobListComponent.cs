using Microsoft.AspNetCore.Mvc;
using Paint.Data;
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
    }
}
