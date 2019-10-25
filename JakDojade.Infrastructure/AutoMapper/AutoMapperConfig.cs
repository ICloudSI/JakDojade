using System;
using AutoMapper;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Commands;
using JakDojade.Infrastructure.Dto;

namespace JakDojade.Infrastructure.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<RegisterCommand, User>();
                cfg.CreateMap<PathBusStops, PathDto>();
                cfg.CreateMap<Node, NodeDto>();

            })
            .CreateMapper();
    }
}
