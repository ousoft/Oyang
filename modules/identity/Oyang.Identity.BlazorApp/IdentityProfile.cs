using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Oyang.Identity.Domain.Entities;
using User = Oyang.Identity.Application.User.Dtos;
using Role = Oyang.Identity.Application.Role.Dtos;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {            
            CreateMap<UserEntity, User.UserDto>();
            CreateMap<User.UserDto, UserEntity>();
            CreateMap<UserEntity, User.AddInputDto>();
            CreateMap<User.AddInputDto, UserEntity>();
            CreateMap<UserEntity, User.UpdateInputDto>();
            CreateMap<User.UpdateInputDto, UserEntity>();

            CreateMap<RoleEntity, Role.RoleDto>();
            CreateMap<Role.RoleDto, RoleEntity>();
            CreateMap<RoleEntity, Role.AddInputDto>();
            CreateMap<Role.AddInputDto, RoleEntity>();
            CreateMap<RoleEntity, Role.UpdateInputDto>();
            CreateMap<Role.UpdateInputDto, RoleEntity>();



        }
    }
}
