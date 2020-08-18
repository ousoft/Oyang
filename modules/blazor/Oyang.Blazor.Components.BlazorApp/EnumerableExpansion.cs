using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Oyang.Blazor.Components.BlazorApp
{
    public static class EnumerableExpansion
    {
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
            {
                source = source.Where(predicate);
            }
            return source;
        }


        public static (List<TSource>, int, int) ToPagination<TSource>(this IEnumerable<TSource> source, int pageIndex, int pageSize, string sortField, bool isAscending)
        {
            var totalCount = source.Count();
            if (!string.IsNullOrWhiteSpace(sortField))
            {
                source = OrderBy(source, isAscending, sortField);
            }

            int pageCount = (int)Math.Ceiling((decimal)totalCount / pageSize);
            if (pageCount > 0 && pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            var list = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return (list.ToList(), totalCount, pageIndex);
        }


        private static IOrderedEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, bool isAscending, string sortField)
        {
            if (string.IsNullOrWhiteSpace(sortField))
            {
                throw new Exception("参数sortField不能null或空字符串");
            }
            var property = typeof(TSource).GetProperty(sortField);
            if (property == null)
            {
                throw new Exception("属性不存在");
            }

            if (property.PropertyType == typeof(string))
            {
                return GanerateOrderBy<TSource, string>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(int))
            {
                return GanerateOrderBy<TSource, int>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(long))
            {
                return GanerateOrderBy<TSource, long>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(float))
            {
                return GanerateOrderBy<TSource, float>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(double))
            {
                return GanerateOrderBy<TSource, double>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(decimal))
            {
                return GanerateOrderBy<TSource, decimal>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(bool))
            {
                return GanerateOrderBy<TSource, bool>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(Guid))
            {
                return GanerateOrderBy<TSource, Guid>(source, isAscending, sortField);
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                return GanerateOrderBy<TSource, DateTime>(source, isAscending, sortField);
            }
            else
            {
                throw new Exception("属性类型不存在");
            }
        }

        private static IOrderedEnumerable<TSource> GanerateOrderBy<TSource, TKey>(IEnumerable<TSource> source, bool isAscending, string sortField)
        {
            var param = Expression.Parameter(typeof(TSource));
            var body = Expression.Property(param, sortField);
            var lambda = Expression.Lambda<Func<TSource, TKey>>(body, param);
            var keySelector = lambda.Compile();
            return isAscending ? Enumerable.OrderBy(source, keySelector) : Enumerable.OrderByDescending(source, keySelector);
        }
    }
}
