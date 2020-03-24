using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Oyang.Identity.Application.User.Dtos;
using Oyang.Identity.Domain.Entities;

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
