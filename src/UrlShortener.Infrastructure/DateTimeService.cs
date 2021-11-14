using System;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Infrastructure;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
