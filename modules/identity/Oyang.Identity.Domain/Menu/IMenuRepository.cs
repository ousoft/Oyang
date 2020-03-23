using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain.Menu
{

    public interface IMenuRepository : IRepository
    {
        List<MenuDto> GetList();
        MenuDto Get(Guid id);
        void Add(AddInputDto input);
        void Update(UpdateInputDto input);
        void Remove(Guid id);
    }
}
