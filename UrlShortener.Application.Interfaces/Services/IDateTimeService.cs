using System;

namespace UrlShortener.Application.Interfaces.Services
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}