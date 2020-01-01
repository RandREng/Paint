using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Paint.Domain;
using RandREng.Paging;
using RandREng.Paging.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<T> GetBidSheetAsync<T>(int i)
        {
            return await context.BidSheets
                .Where(b => b.Id == i)
                .Include(b => b.Job).ThenInclude(j => j.Client).ThenInclude(c => c.Parent)
                .Include(b => b.Areas).ThenInclude(a => a.Items)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<BidSheet> GetBidSheetAsync(int i)
        {
            return await context.BidSheets
                .Where(b => b.Id == i)
                .Include(b => b.Job).ThenInclude(j => j.Client).ThenInclude(c => c.Parent)
                .Include(b => b.Areas).ThenInclude(a => a.Items)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<T>> GetBidListAsync<T>(int page, int pageSize, int? clientId = null) where T : class
        {
            if (clientId is null)
            {
                return await context.BidSheets
                    .Include(b => b.Job).ThenInclude(j => j.Client).ThenInclude(c => c.Parent)
                    .ToPageResultAsync<BidSheet, Client, T>(page, pageSize, mapper.ConfigurationProvider);
            }
            else
            {
                return await context.BidSheets.Where(b => b.Job.ClientId == clientId.Value)
                    .ToPageResultAsync<BidSheet, T>(page, pageSize, mapper.ConfigurationProvider);
            }
        }

        public async Task<PagedResult<BidSheet>> GetBidListAsync(int page, int pageSize, int? clientId = null)
        {
            if (clientId is null)
            {
                return await context.BidSheets
                .Include(b => b.Job).ThenInclude(j => j.Client).ThenInclude(c => c.Parent)
                .ToPageResultAsync<BidSheet>(page, pageSize);
            }
            else
            {
                return await context.BidSheets.Include(b => b.Job).Where(b => b.Job.ClientId == clientId.Value)
                    .ToPageResultAsync<BidSheet>(page, pageSize);
            }
        }

        public async Task<PagedResult<T>> GetJobListAsync<T>(int page, int pageSize, int? clientId = null) where T : class
        {
            if (clientId is null)
            {
                return await context.Jobs
                    .Include(j => j.Client).ThenInclude(c => c.Parent)
                    .ToPageResultAsync<Job, Client, T>(page, pageSize, mapper.ConfigurationProvider);
            }
            else
            {
                return await context.Jobs.Where(j => j.ClientId == clientId.Value)
                    .ToPageResultAsync<Job, T>(page, pageSize, mapper.ConfigurationProvider);
            }
        }

        public async Task<PagedResult<Job>> GetJobListAsync(int page, int pageSize, int? clientId = null)
        {
            if (clientId is null)
            {
                return await context.Jobs
                .Include(j => j.Client).ThenInclude(c => c.Parent)
                .ToPageResultAsync<Job>(page, pageSize);
            }
            else
            {
                return await context.Jobs.Where(j => j.ClientId == clientId.Value)
                    .ToPageResultAsync<Job>(page, pageSize);
            }
        }

    }
}
