using System.Reflection;
using AutoMapper;
using UrlShortener.Application.Interfaces.Extensions;

namespace UrlShortener.WebUI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}