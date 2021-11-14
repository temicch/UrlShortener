using System.Reflection;
using AutoMapper;
using UrlShortener.Application.Interfaces.Extensions;

namespace UrlShortener.Application.Implementation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
