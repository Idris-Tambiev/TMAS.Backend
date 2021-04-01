using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.DTO;
using TMAS.DB.Models;
using AutoMapper;

namespace TMAS.BLL.Mapper
{
   public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrateUserDto, User>()
                    .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.UserName))
                    .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Lastname, act => act.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                    .ForMember(dest => dest.EmailConfirmed, act => act.MapFrom(src => true))
                    .ForMember(dest => dest.LockoutEnabled, act => act.MapFrom(src => false));
        }
    }
}
