﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;

namespace RandREng.Paging
{
    public static partial class PagedResultEFCoreExtensions
    {
        public static PagedResult<T> ToPageResult<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static PagedResult<U> ToPageResult<T, U>(this IQueryable<T> query, int page, int pageSize, MapperConfiguration config) where U : class
        {
            var result = new PagedResult<U>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip)
                                  .Take(pageSize)
                                  .ProjectTo<U>(config)
                                  .ToList();
            return result;
        }

    }
}
