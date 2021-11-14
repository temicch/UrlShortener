using System;

namespace UrlShortener.Application.Interfaces.Services;

/// <summary>
///     Represents service for <see cref="DateTime.Now" />
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    ///     Current time
    /// </summary>
    DateTime Now { get; }
}
