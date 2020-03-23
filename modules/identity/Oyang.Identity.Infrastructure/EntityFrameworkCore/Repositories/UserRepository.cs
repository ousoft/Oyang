using AutoMapper;
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.User;
using Oyang.Identity.Infrastructure.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore.Repositories
{
    public class UserRepository : EfRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IdentityDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public void Add(AddInputDto input)
        {
            var entity = Mapper.Map<UserEntity>(input);
            DbContext.AddAttachAudit(entity);
        }

        public bool ExistUser(string loginName)
        {
            return Queryable.Any(t => t.LoginName == loginName);
        }

        public UserDto Get(Guid id)
        {
            var entity =  Queryable.Single(t => t.Id == id);
            var dto = Mapper.Map<UserDto>(entity);
            return dto;
        }

        public Pagination<UserDto> GetList(GetListInputDto input)
        {
            var query = Queryable;
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.LoginName), t => t.LoginName.Contains(input.LoginName));
            var (list, totalCount) = query.ToPagination(input);
            var listDto = Mapper.Map<List<UserDto>>(list);
            var pagination = new Pagination<UserDto>(input, listDto, totalCount);
            return pagination;
        }

        public void Remove(Guid id)
        {
            var entity = DbContext.Set<UserEntity>().Find(id);
            DbContext.RemoveAttachAudit(entity);
        }

        public void ResetPassword(ResetPasswordInputDto input)
        {
            var entity = DbContext.Set<UserEntity>().Find(input.Id);
            Mapper.Map(input, entity);
        }

        public void SetRole(SetRoleInputDto input)
        {
            var list = DbContext.Set<UserRoleEntity>().Where(t => t.UserId == input.UserId).ToList();
            var listRemove = list.Where(t => !input.RoleIds.Contains(t.RoleId));
            DbContext.Set<UserRoleEntity>().RemoveRange(listRemove);

            var listExistRoleId = list.Select(t => t.RoleId).ToList();
            var listAdd = input.RoleIds.Where(t => !listExistRoleId.Contains(t))
                .Select(t => new UserRoleEntity()
                {
                    Id = Guid.NewGuid(),
                    UserId = input.UserId,
                    RoleId = t,
                });
            DbContext.Set<UserRoleEntity>().AddRange(listAdd);
        }

        public void Update(UpdateInputDto input)
        {
            var entity = DbContext.Set<UserEntity>().Find(input.Id);
            Mapper.Map(input, entity);
        }
    }
}
