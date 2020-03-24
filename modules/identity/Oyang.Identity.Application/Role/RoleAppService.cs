using Oyang.Identity.Domain;
using Oyang.Identity.Application.Role.Dtos;
using System;
using Oyang.Identity.Domain.Repositories;
using Oyang.Identity.Domain.Entities;
using System.Collections.Generic;

namespace Oyang.Identity.Application.Role
{
    public class RoleAppService : BaseAppService, IRoleAppService
    {
        //private readonly IRoleRepository _repository;
        //private readonly IObjectMapper _objectMapper;
        //public RoleAppService(IRoleRepository roleRepository)
        //{
        //    _repository = roleRepository;
        //}

        //public void Add(AddInputDto input)
        //{
        //    ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Name), "名称不能为空");
        //    ValidationObject.Validate(_repository.ExistRole(input.Name), "名称已存在");
        //    _repository.Add(null);
        //}

        //public Pagination<RoleDto> GetList(GetListInputDto input)
        //{
        //    var pagination = _repository.GetList(input, input.Name);
        //    var list = _objectMapper.Map<List<RoleEntity>, List<RoleDto>>(pagination.Items);
        //    return new Pagination<RoleDto>(pagination, pagination.TotalCount, list);
        //}

        //public void Remove(Guid id)
        //{
        //    _repository.Remove(id);
        //}

        //public void SetPermission(SetPermissionInputDto input)
        //{
        //    _repository.SetPermission(input);
        //}

        //public void SetUser(SetUserInputDto input)
        //{
        //    _repository.SetUser(input.RoleId,input.UserIds);
        //}

        //public void Update(UpdateInputDto input)
        //{
        //    ValidationObject.Validate(string.IsNullOrWhiteSpace(input.Name), "名称不能为空");
        //    ValidationObject.Validate(_repository.ExistRole(input.Id, input.Name), "名称已存在");
        //    _repository.Update(null);
        //}

    }
}
