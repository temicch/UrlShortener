using System.Reflection;
using AutoMapper;
using UrlShortener.Application.Interfaces.Extensions;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LinkRequestedEvent, LinkClick>()
            .ForMember(x => x.LinkId, y => y.MapFrom(z => z.Payload.Id));

        this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}