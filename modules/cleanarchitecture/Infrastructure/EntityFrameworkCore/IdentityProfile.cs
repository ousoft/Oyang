using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.User.Dtos;
using AutoMapper;
using Infrastructure.EntityFrameworkCore.Entities;

namespace Infrastructure.EntityFrameworkCore
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
