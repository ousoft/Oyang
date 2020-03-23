using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Role
{
    public interface IRoleRepository : IRepository
    {
        Pagination<RoleDto> GetList(GetListInputDto input);
        RoleDto Get(Guid id);
        void Add(AddInputDto input);
        void Update(UpdateInputDto input);
        void Remove(Guid id);
        bool ExistRole(string name);
        bool ExistRole(Guid id, string name);
        void SetUser(SetUserInputDto input);
        void SetPermission(SetPermissionInputDto input);
    }
}
