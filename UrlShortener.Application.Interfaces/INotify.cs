using System;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces
{
    public interface INotify<TResponse>
    {
        Func<TResponse, DateTime?, DomainEvent<TResponse>> Event { get; }
    }
}