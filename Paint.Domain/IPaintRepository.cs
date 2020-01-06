using RandREng.Paging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Domain
{
    public interface IPaintRepository
    {
        public Task<T> GetBidSheetAsync<T>(int i);
        public Task<BidSheet> GetBidSheetAsync(int i);

        public Task<PagedResult<T>> GetBidListAsync<T>(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null) where T: class;
        public Task<PagedResult<BidSheet>> GetBidListAsync(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null);

        public Task<PagedResult<T>> GetJobListAsync<T>(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null) where T : class;
        public Task<PagedResult<Job>> GetJobListAsync(int page, int pageSize, string sortColumn, string sortDirection, int? clientId = null);
    }
}
