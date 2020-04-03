using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Repositories;
using Oyang.Identity.Application.User.Dtos;
using System;
using System.Collections.Generic;
using Oyang.Identity.Domain.Entities;
using System.Linq;

namespace Oyang.Identity.Application.User
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<UserRoleEntity> _userRoleRepository;
        private readonly IObjectMapper _objectMapper;

        public UserAppService(
            IRepository<UserEntity> userRepository,
            IRepository<UserRoleEntity> userRoleRepository
            )
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public void Add(AddInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.LoginName), "登录名不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password), "密码不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password2), "确认密码不能为空");
            ValidationObject.Validate(input.Password != input.Password2, "密码和确认密码不一致");
            var exist = _userRepository.Queryable.Any(t => t.LoginName == input.LoginName);
            ValidationObject.Validate(exist, "登录名已存在");
            var entity = _objectMapper.Map<AddInputDto, UserEntity>(input);
            _userRepository.Add(entity);
        }

        public Pagination<UserDto> GetList(GetListInputDto input)
        {
            var query = _userRepository.QueryableAsNoTracking;
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.LoginName), t => t.LoginName.Contains(input.LoginName));
            var (listEntity, totalCount) = query.ToPagination(input);
            //var listDto = _objectMapper.Map<List<UserEntity>, List<UserDto>>(listEntity);
            var listDto = listEntity.Select(t => new UserDto() { Id = t.Id, LoginName = t.LoginName, NickName = "无", PasswordHash = t.PasswordHash }).ToList();
            var pagination = new Pagination<UserDto>(input, totalCount, listDto);
            return pagination;
        }

        public void Remove(Guid id)
        {
            _userRepository.Remove(id);
        }

        public void ResetPassword(ResetPasswordInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password), "密码不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password2), "确认密码不能为空");
            ValidationObject.Validate(input.Password != input.Password2, "密码和确认密码不一致");
            var entity = _userRepository.Find(input.Id);
            entity.PasswordHash = input.Password;
        }

        public void SetRole(SetRoleInputDto input)
        {
            var list = _userRoleRepository.Queryable.Where(t => t.UserId == input.UserId).ToList();
            var listRemove = list.Where(t => !input.RoleIds.Contains(t.RoleId)).ToArray();
            _userRoleRepository.RemoveRange(listRemove);

            var listExistRoleId = list.Select(t => t.RoleId).ToList();
            var listAdd = input.RoleIds.Where(t => !listExistRoleId.Contains(t))
                .Select(t => new UserRoleEntity()
                {
                    Id = Guid.NewGuid(),
                    UserId = input.UserId,
                    RoleId = t,
                });
            _userRoleRepository.AddRange(listAdd);
        }

        public void Update(UpdateInputDto input)
        {
            var entity = _userRepository.Find(input.Id);
            _objectMapper.Map(input, entity);
            _userRepository.Add(entity);

        }
    }
}
