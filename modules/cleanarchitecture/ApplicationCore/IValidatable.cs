using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public interface IValidatable
    {
        void Validate();
    }
    public interface IValidatable<TRepository> where TRepository : IRepository
    {
        void Validate(TRepository repository);
    }
}
