using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Paint.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paint.Data
{
    public class PaintRepository : IPaintRepository
    {
        private readonly IMapper mapper;
        private readonly Context context;

        public PaintRepository(IMapper mapper, Context context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public T GetBidSheet<T>(int i)
        {
            return context.BidSheets.Where(b => b.Id == i).Include(b => b.Job).ThenInclude(j => j.Client).ThenInclude(c => c.Parent).ProjectTo<T>(mapper.ConfigurationProvider).FirstOrDefault();
        }

        public BidSheet GetBidSheet(int i)
        {
            return context.BidSheets.Where(b => b.Id == i).Include(b => b.Job).ThenInclude(j => j.Client).ThenInclude(c => c.Parent).FirstOrDefault();
        }
    }
}
