using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces;
using UrlShortener.WebUI;
using Xunit;

namespace UrlShortener.Common.Tests.Common;

public abstract class IntegrationTestBase : IClassFixture<TestFixture<Startup>>, IDisposable
{
    protected readonly IDbContext _dbContext;
    protected readonly IMediator _mediator;
    protected readonly IServiceScope _scope;
    protected readonly IServiceProvider _services;

    protected readonly IDbContextTransaction transaction;

    protected IntegrationTestBase(TestFixture<Startup> testFixture)
    {
        _scope = testFixture.Server.Services.CreateScope();
        _services = _scope.ServiceProvider;

        _dbContext = _services.GetRequiredService<IDbContext>();
        _mediator = _services.GetRequiredService<IMediator>();

        transaction = ((DbContext)_dbContext).Database.BeginTransaction();
    }

    public virtual void Dispose()
    {
        transaction.Dispose();
        _scope.Dispose();
    }
}