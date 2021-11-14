using AutoMapper;

namespace UrlShortener.Application.Interfaces.Mapping;

/// <summary>
///     <para>
///         Mapping this type to <typeparamref name="TTo" />
///     </para>
/// </summary>
public interface IMapTo<TTo>
{
    /// <summary>
    ///     <inheritdoc cref="AutoMapper.Configuration.IProfileConfiguration" />
    /// </summary>
    /// <param name="profile">
    ///     <inheritdoc cref="Profile" />
    /// </param>
    void Mapping(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(TTo));
    }
}
