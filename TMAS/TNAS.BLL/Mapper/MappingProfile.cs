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
            CreateMap<NewBoardDto, CreatedBoardDto>()
                    .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
                    .ForMember(dest => dest.BoardUserId, act => act.MapFrom(src => src.UserBoardId))
                    .ForMember(dest => dest.CreatedDate, act => act.MapFrom(src => DateTime.Now));
        }
    }
}
