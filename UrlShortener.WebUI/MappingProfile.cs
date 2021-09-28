using System.Reflection;
using AutoMapper;
using UrlShortener.Application.Implementation;

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