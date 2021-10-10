using AutoMapper;

namespace UrlShortener.Application.Interfaces.Mapping
{
    /// <summary>
    ///     <para>
    ///         Mapping from <typeparamref name="TFrom" /> to this type
    ///     </para>
    /// </summary>
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