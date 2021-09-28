using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.Implementation.Common.Behaviours
{
    public class NotifyBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : INotify<TResponse>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IDomainEventService _domainEventService;

        public NotifyBehavior(IDomainEventService domainEventService, IDateTimeService dateTimeService)
        {
            _domainEventService = domainEventService;
            _dateTimeService = dateTimeService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            await _domainEventService.PublishAsync(request.Event(response, _dateTimeService.Now), cancellationToken);

            return response;
        }
    }
}