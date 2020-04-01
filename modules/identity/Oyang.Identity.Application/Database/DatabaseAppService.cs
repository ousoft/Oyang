using Oyang.Identity.Domain;
using Oyang.Identity.Domain.Entities;
using Oyang.Identity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Oyang.Identity.Application.Database
{
    public class DatabaseAppService : BaseAppService, IDatabaseAppService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IRepository<PermissionEntity> _permissionRepository;
        public DatabaseAppService(
            IDatabaseRepository databaseRepository,
            IRepository<PermissionEntity> permissionRepository
            )
        {
            _databaseRepository = databaseRepository;
            _permissionRepository = permissionRepository;
        }

        public bool GenerateDatabase()
        {
            return _databaseRepository.GenerateDatabase();
        }

        public void GenerateSeedData()
        {
            GenerateSeedDataByPermission();
        }

        public void GenerateSeedDataByPermission()
        {
            var list = new List<PermissionEntity>();
            var appServiceTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsInterface && t.BaseType == typeof(IApplicationService));
            foreach (var appService in appServiceTypes)
            {
                var methods = appService.GetMethods();
                foreach (var method in methods)
                {
                    var attribute = method.GetCustomAttribute<PermissionAttribute>();
                    if (attribute != null && !list.Exists(t => t.Code == attribute.Code))
                    {
                        list.Add(new PermissionEntity()
                        {
                            Id = Guid.NewGuid(),
                            Code = attribute.Code,
                            Name = attribute.Name,
                            IsEnable = true,
                        });
                    }
                }
            }
            _permissionRepository.AddRange(list);
        }
    }
}
