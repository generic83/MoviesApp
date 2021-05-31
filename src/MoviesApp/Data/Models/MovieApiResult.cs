using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesApp.Data.Models.Entities;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace MoviesApp.Data.Models
{
    public class MovieApiResult
    {
        private MovieApiResult(List<Movie> data, int count, int pageIndex, int pageSize)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public static async Task<MovieApiResult> CreateAsync(IQueryable<Movie> source,
                                                             int pageIndex,
                                                             int pageSize,
                                                             string sortColumn,
                                                             string sortOrder,
                                                             string titleFilterQuery)
        {
            if (!string.IsNullOrEmpty(titleFilterQuery))
            {
                source = source.Where(x => x.Title.ToLowerInvariant().Contains(titleFilterQuery.ToLowerInvariant()));
            }

            if (!string.IsNullOrEmpty(sortColumn) && IsValidProperty(sortColumn))
            {
                sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC" ?
                    "ASC" :
                    "DESC";

                source = source.OrderBy($"{sortColumn} {sortOrder}");
            }

            var count = await source.CountAsync();

            var data = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return new MovieApiResult(data, count, pageIndex, pageSize);
        }

        public List<Movie> Data { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => PageIndex + 1 < TotalPages;

        static bool IsValidProperty(string propertyName, bool throwExceptionIfNotFound = true)
        {
            var proppertyInfo = typeof(Movie).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (proppertyInfo == null && throwExceptionIfNotFound)
            {
                throw new NotSupportedException($"ERROR: Property '{propertyName}' does not exist.");
            }

            return proppertyInfo != null;
        }
    }
}