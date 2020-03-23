using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Database;
using Oyang.Identity.Domain.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Oyang.Identity.Application.Database
{
    public class DatabaseAppService : BaseAppService, IDatabaseAppService
    {
        private readonly IDatabaseRepository _repository;
        public DatabaseAppService(IDatabaseRepository repository)
        {
            _repository = repository;
        }

        public void ClearSeedData()
        {
            throw new NotImplementedException();
        }

        public void GenerateDatabase()
        {
            _repository.GenerateDatabase();
        }

        public void GenerateSeedData()
        {
            throw new NotImplementedException();
        }

        public void RegenerateSeedDataByPermission()
        {
            _repository.CleanSeedDataByPermission();

            var list = new List<PermissionDto>();
            var appServiceTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsInterface && t.BaseType == typeof(IApplicationService));
            foreach (var appService in appServiceTypes)
            {
                var methods = appService.GetMethods();
                foreach (var method in methods)
                {
                    var attribute = method.GetCustomAttribute<PermissionAttribute>();
                    if (attribute != null && !list.Exists(t => t.Code == attribute.Code))
                    {
                        list.Add(new PermissionDto()
                        {
                            Id = Guid.NewGuid(),
                            Code = attribute.Code,
                            Name = attribute.Name,
                            IsEnable = true,
                        });
                    }
                }
            }
            _repository.GenerateSeedDataByPermission(list);
        }
    }
}
