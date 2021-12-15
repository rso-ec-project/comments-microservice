using AutoMapper;
using Comments.Domain.CommentAggregate;
using System;

namespace Comments.Application.Comments
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opts => opts.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opts => opts.MapFrom(src => src.Rating))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ChargingStationId, opts => opts.MapFrom(src => src.ChargingStationId))
                ;

            CreateMap<CommentPostDto, Comment>()
                .ForMember(dest => dest.Content, opts => opts.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opts => opts.MapFrom(src => src.Rating))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ChargingStationId, opts => opts.MapFrom(src => src.ChargingStationId))
                ;
        }
    }
}
