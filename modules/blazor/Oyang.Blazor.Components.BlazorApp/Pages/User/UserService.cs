using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oyang.Blazor.Components.BlazorApp.Pages.User
{
    public class UserService
    {
        private static List<UserEntity> _listUser = null;

        static UserService()
        {
            Random r = new Random();
            _listUser = new List<UserEntity>();
            for (int i = 0; i < 106; i++)
            {
                _listUser.Add(new UserEntity() { Id = Guid.NewGuid(), LoginName = "testuser" + i.ToString("000"), Password = "123", CreateTime = new DateTime(2020, r.Next(1, 12), r.Next(1, 30)) });
            }
        }

        public (List<UserEntity>, int, int) GetList(int pageIndex, int pageSize, string sortField, bool isAscending, string loginName)
        {
            IEnumerable<UserEntity> query = _listUser;
            query = query.WhereIf(!string.IsNullOrWhiteSpace(loginName), t => t.LoginName.Contains(loginName));
            var (list, totalCount, pageIndex2) = query.ToPagination(pageIndex, pageSize, sortField, isAscending);
            return (list, totalCount, pageIndex2);
        }


        private IOrderedEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, bool isAscending, string sortField)
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

        private IOrderedEnumerable<TSource> GanerateOrderBy<TSource, TKey>(IEnumerable<TSource> source, bool isAscending, string sortField)
        {
            var param = Expression.Parameter(typeof(TSource));
            var body = Expression.Property(param, sortField);
            var lambda = Expression.Lambda<Func<TSource, TKey>>(body, param);
            var keySelector = lambda.Compile();
            return isAscending ? Enumerable.OrderBy(source, keySelector) : Enumerable.OrderByDescending(source, keySelector);
        }
    }
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
    }

}
