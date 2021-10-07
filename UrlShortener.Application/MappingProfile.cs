using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using UrlShortener.Application.Interfaces.Mapping;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.Implementation
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LinkRequestedEvent, LinkClick>()
                .ForMember(x => x.Link, y => y.MapFrom(z => z.Payload));

            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public static class MappingExtensions
    {
        public static void ApplyMappingsFromAssembly(this Profile profile, Assembly assembly)
        {
            var mappedTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>) ||
                                        i.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                .ToList();

            foreach (var mappedType in mappedTypes)
            {
                var instance = Activator.CreateInstance(mappedType);

                var mappingInterfaces = mappedType
                    .GetInterfaces()
                    .Where(x => x.Name.Contains(nameof(IMapTo<object>)) ||
                                x.Name.Contains(nameof(IMapFrom<object>)));

                foreach (var @interface in mappingInterfaces)
                {
                    var method = @interface.GetMethod(nameof(IMapFrom<object>.Mapping));

                    method.Invoke(instance, new object[] { profile });
                }
            }
        }
    }
}