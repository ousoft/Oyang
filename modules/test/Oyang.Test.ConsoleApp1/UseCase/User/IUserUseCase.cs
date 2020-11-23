using Oyang.Test.ConsoleApp1.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Test.ConsoleApp1.UseCase.User
{
    public interface IUserUseCase
    {
        void Add(UserEntity entity);
        void Update(UserEntity entity);
        void Remove(UserEntity entity);
        UserEntity Get(Guid id);
        List<UserEntity> GetList(Guid id);
    }
}
