﻿using MediatR;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.Implementation.Common
{
    public interface IPaginatedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, PaginatedList<TResponse>>
        where TRequest : IRequest<PaginatedList<TResponse>>
    {
    }
}