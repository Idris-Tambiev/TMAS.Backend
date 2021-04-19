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
                    .ForMember(dest => dest.EmailConfirmed, act => act.MapFrom(src => false))
                    .ForMember(dest => dest.LockoutEnabled, act => act.MapFrom(src => false));

            CreateMap<Card, CardViewDTO>()
                    .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
                    .ForMember(dest => dest.Text, act => act.MapFrom(src => src.Text))
                    .ForMember(dest => dest.IsDone, act => act.MapFrom(src => src.IsDone))
                    .ForMember(dest => dest.ColumnId, act => act.MapFrom(src => src.ColumnId))
                    .ForMember(dest => dest.SortBy, act => act.MapFrom(src => src.SortBy));

            CreateMap<Board, BoardViewDTO>()
                    .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title));

            CreateMap<Column, ColumnViewDTO>()
                    .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
                    .ForMember(dest => dest.SortBy, act => act.MapFrom(src => src.SortBy))
                    .ForMember(dest => dest.BoardId, act => act.MapFrom(src => src.BoardId));

            CreateMap<ColumnViewDTO, Column>()
                    .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
                    .ForMember(dest => dest.SortBy, act => act.MapFrom(src => src.SortBy))
                    .ForMember(dest => dest.BoardId, act => act.MapFrom(src => src.BoardId))
                    .ForMember(dest => dest.CreatedDate, act => act.MapFrom(src => DateTime.Now))
                    .ForMember(dest => dest.IsActive, act => act.MapFrom(src => true));

            CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                    .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.Lastname))
                    .ForMember(dest => dest.Photo, act => act.MapFrom(src => src.Photo));

            CreateMap<History, HistoryViewDTO>()
                 .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ActionObject, act => act.MapFrom(src => src.ActionObject))
                    .ForMember(dest => dest.ActionType, act => act.MapFrom(src => src.ActionType))
                    .ForMember(dest => dest.DestinationAction, act => act.MapFrom(src => src.DestinationAction))
                    .ForMember(dest => dest.SourceAction, act => act.MapFrom(src => src.SourceAction))
                    .ForMember(dest => dest.CreatedDate, act => act.MapFrom(src => src.CreatedDate));
        }
    }
}
