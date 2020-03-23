using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Role;
using System;

namespace Oyang.Identity.Application.Role
{
    public class RoleAppService : BaseAppService, IRoleAppService
    {
        private readonly IRoleRepository _repository;
        public RoleAppService(IRoleRepository roleRepository)
        {
            _repository = roleRepository;
        }

        public void Add(AddInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Name), "名称不能为空");
            ValidationObject.Validate(_repository.ExistRole(input.Name), "名称已存在");
            _repository.Add(input);
        }

        public Pagination<RoleDto> GetList(GetListInputDto input)
        {
            return _repository.GetList(input);
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }

        public void SetPermission(SetPermissionInputDto input)
        {
            _repository.SetPermission(input);
        }

        public void SetUser(SetUserInputDto input)
        {
            _repository.SetUser(input);
        }

        public void Update(UpdateInputDto input)
        {
            ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Name), "名称不能为空");
            ValidationObject.Validate(_repository.ExistRole(input.Id, input.Name), "名称已存在");
            _repository.Update(input);
        }

    }
}
