using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Oyang.Identity.Domain.User;
using Oyang.Identity.Infrastructure.EntityFrameworkCore.Entities;

namespace Oyang.Identity.Infrastructure.EntityFrameworkCore
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>();
        }
    }
}
