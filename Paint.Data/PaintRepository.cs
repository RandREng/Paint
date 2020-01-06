using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        public async Task<PagedResult<T>> GetBidListAsync<T>(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null) where T : class
        {
            IQueryable<BidSheet> query = buildBidListQuery(clientId, sortColumn, sortDirection);
            return await query.ToPageResultAsync<BidSheet, T>(page, pageSize, mapper.ConfigurationProvider);

        }

        public async Task<PagedResult<BidSheet>> GetBidListAsync(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null)
        {
            IQueryable<BidSheet> query = buildBidListQuery(clientId, sortColumn, sortDirection);
            return await query.ToPageResultAsync<BidSheet>(page, pageSize);
        }

        private IQueryable<BidSheet> buildBidListQuery(int? clientId, string sortColumn, string sortDirection)
        {
            IQueryable<BidSheet> query = null;
            if (clientId is null)
            {
                query = context.BidSheets
                .Include(b => b.Job).ThenInclude(j => j.Client).ThenInclude(c => c.Parent);
            }
            else
            {
                query = context.BidSheets.Include(b => b.Job).Where(b => b.Job.ClientId == clientId.Value);
            }

            if (sortDirection == "asc")
            {
                if (sortColumn == "name")
                {
                    query = query.OrderBy(b => b.Job.Client.LastName).ThenBy(b => b.Job.Client.FirstName);
                }
                else if (sortColumn == "address")
                {
                    query = query.OrderBy(b => b.Job.Address.Line1);
                }
                else if (sortColumn == "city")
                {
                    query = query.OrderBy(b => b.Job.Address.City);
                }
                else if (sortColumn == "budget")
                {
                    query = query.OrderBy(b => b.RenoTotal);
                }
            }
            else if (sortDirection == "desc")
            {
                if (sortColumn == "name")
                {
                    query = query.OrderByDescending(b => b.Job.Client.LastName).ThenByDescending(b => b.Job.Client.FirstName);
                }
                else if (sortColumn == "address")
                {
                    query = query.OrderByDescending(b => b.Job.Address.Line1);
                }
                else if (sortColumn == "city")
                {
                    query = query.OrderByDescending(b => b.Job.Address.City);
                }
                else if (sortColumn == "budget")
                {
                    query = query.OrderByDescending(b => b.RenoTotal);
                }
            }
            return query;

        }


        public async Task<PagedResult<T>> GetJobListAsync<T>(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null) where T : class
        {
            IQueryable<Job> query = buildJobListQuery(clientId, sortColumn, sortDirection);
            return await query.ToPageResultAsync<Job, T>(page, pageSize, mapper.ConfigurationProvider);
        }

        public async Task<PagedResult<Job>> GetJobListAsync(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null)
        {
            IQueryable<Job> query = buildJobListQuery(clientId, sortColumn, sortDirection);
            return await query.ToPageResultAsync<Job>(page, pageSize);
        }

        private IQueryable<Job> buildJobListQuery(int? clientId, string sortColumn, string sortDirection)
        {
            IQueryable<Job> query = null;
            if (clientId is null)
            {
                query = context.Jobs
                    .Include(j => j.Client).ThenInclude(c => c.Parent);
            }
            else
            {
                query = context.Jobs
                    .Where(j => j.ClientId == clientId.Value);
            }

            if (sortDirection == "asc")
            {
                if (sortColumn == "name")
                {
                    query = query.OrderBy(j => j.Client.LastName).ThenBy(j => j.Client.FirstName);
                }
                else if (sortColumn == "address")
                {
                    query = query.OrderBy(j => j.Address.Line1);
                }
                else if (sortColumn == "city")
                {
                    query = query.OrderBy(j => j.Address.City);
                }
            }
            else if (sortDirection == "desc")
            {
                if (sortColumn == "name")
                {
                    query = query.OrderByDescending(j => j.Client.LastName).ThenByDescending(j => j.Client.FirstName);
                }
                else if (sortColumn == "address")
                {
                    query = query.OrderByDescending(j => j.Address.Line1);
                }
                else if (sortColumn == "city")
                {
                    query = query.OrderByDescending(j => j.Address.City);
                }
            }
            return query;

        }

    }
}
