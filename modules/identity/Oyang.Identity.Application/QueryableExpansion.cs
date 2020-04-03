using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Oyang.Identity.Application
{
    public static class QueryableExpansion
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
            {
                source = source.Where(predicate);
            }
            return source;
        }

        public static (List<TSource>, int) ToPagination<TSource>(this IQueryable<TSource> source, Pagination pagination)
        {
            var query = source;
            int totalCount = query.Count();
            int pageCount = (int)Math.Ceiling((decimal)totalCount / pagination.PageSize);
            if (pageCount > 0 && pagination.PageIndex > pageCount)
            {
                pagination.PageIndex = pageCount;
            }

            if (!string.IsNullOrWhiteSpace(pagination.SortField))
            {
                query = OrderBy(query, pagination.IsAscending, pagination.SortField);
                if (pagination.SecondarySort != null)
                {
                    foreach (var item in pagination.SecondarySort)
                    {
                        query = ThenBy(query, item.IsAscending, item.SortField);
                    }
                }
            }

            query = query.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize);
            var list = query.ToList();
            return (list, totalCount);
        }

        private static IQueryable<TSource> OrderBy<TSource>(IQueryable<TSource> source, bool isAscending, string sortField)
        {
            var param = Expression.Parameter(typeof(TSource));            
            var body = Expression.Property(param, sortField);
            dynamic keySelector = Expression.Lambda(body, param);
            source = isAscending ? Queryable.OrderBy(source, keySelector) : Queryable.OrderByDescending(source, keySelector);
            return source;
        }
        private static IQueryable<TSource> ThenBy<TSource>(IQueryable<TSource> source, bool isAscending, string sortField)
        {
            var param = Expression.Parameter(typeof(TSource));
            var body = Expression.Property(param, sortField);
            dynamic keySelector = Expression.Lambda(body, param);
            source = isAscending ? Queryable.ThenBy(source, keySelector) : Queryable.ThenByDescending(source, keySelector);
            return source;
        }
    }
}
