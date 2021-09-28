using AutoMapper;

namespace UrlShortener.Application.Interfaces.Mapping
{
    public interface IMapFrom<TFrom>
    {
        /// <summary>
        ///     <inheritdoc cref="AutoMapper.Configuration.IProfileConfiguration" />
        /// </summary>
        /// <param name="profile">
        ///     <inheritdoc cref="Profile" />
        /// </param>
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(TFrom), GetType());
        }
    }
}