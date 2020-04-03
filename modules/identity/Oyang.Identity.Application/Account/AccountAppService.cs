using Oyang.Identity.Application.Account.Dtos;
using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Entities;
using System.Linq;
using System;

namespace Oyang.Identity.Application.Account
{
    public class AccountAppService : BaseAppService, IAccountAppService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<RoleEntity> _roleRepository;
        private readonly IRepository<PermissionEntity> _permissionRepository;
        private readonly IRepository<UserRoleEntity> _userRoleRepository;
        private readonly IRepository<RolePermissionEntity> _rolePermissionRepository;

        public AccountAppService(
            IRepository<UserEntity> userRepository,
            IRepository<RoleEntity> roleRepository,
            IRepository<PermissionEntity> permissionRepository,
            IRepository<UserRoleEntity> userRoleRepository,
            IRepository<RolePermissionEntity> rolePermissionRepository
            )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public ICurrentUser Login(LoginInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.LoginName), "登录名不能为空");
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Password), "密码不能为空");
            var userDto = _userRepository.QueryableAsNoTracking.SingleOrDefault(t => t.LoginName == input.LoginName);
            ValidationObject.Validate(userDto == null, "登录名不存在");
            ValidationObject.Validate(userDto.PasswordHash != input.Password, "密码不正确");

            var currentUser = Get(userDto.Id);
            return currentUser;
        }

        public ICurrentUser Get(Guid userId)
        {
            //var userEntity = _userRepository.QueryableAsNoTracking.SingleOrDefault(t => t.Id == userId);

            //var listRoleId = _userRoleRepository.QueryableAsNoTracking.Where(t => t.UserId == userEntity.Id).Select(t => t.RoleId).ToList();
            //var listRoleEntity =_roleRepository.QueryableAsNoTracking.Where(t => listRoleId.Contains(t.Id)).ToList();
            //var listRoleDto = Mapper.Map<List<RoleDto>>(listRoleEntity);

            //var listPermissionId = DbContext.Queryable<RolePermissionEntity>().AsNoTracking().Where(t => listRoleId.Contains(t.RoleId)).Select(t => t.PermissionId).Distinct().ToList();
            //var listPermissionEntity = DbContext.Queryable<PermissionEntity>().AsNoTracking().Where(t => listPermissionId.Contains(t.Id)).ToList();
            //var listPermissionDto = Mapper.Map<List<PermissionModel>>(listPermissionEntity);

            //var listMenuId = DbContext.Queryable<PermissionEntity>().AsNoTracking().Where(t => t.MenuId != null).Select(t => t.MenuId.Value).ToList();
            //var listMenuEntity = DbContext.Queryable<MenuEntity>().AsNoTracking().Where(t => listMenuId.Contains(t.Id)).ToList();
            //var listMenuDto = Mapper.Map<List<MenuDto>>(listMenuEntity);

            //var currentUser = new CurrentUser(userEntity.Id, userEntity.LoginName, userEntity.PasswordHash, listRoleDto, listPermissionDto, listMenuDto);
            //return currentUser;
            return null;
        }
    }
}
