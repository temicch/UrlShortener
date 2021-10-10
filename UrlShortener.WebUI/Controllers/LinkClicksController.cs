using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Implementation.LinkClicks.Queries.GetClicksStatistic;
using UrlShortener.Application.Interfaces.Paginated;

namespace UrlShortener.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkClicksController : Controller
    {
        private readonly IMediator _mediator;

        public LinkClicksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get information about clicks on links
        /// </summary>
        /// <remarks>
        ///     Information such as the link itself, the date of the last click on it and the
        ///     total number of clicks will be received
        /// </remarks>
        [ProducesResponseType(typeof(PaginatedList<GetClicksResponse>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ValidationProblemDetails))]
        [HttpGet]
        public async Task<IActionResult> GetClicks([FromQuery] GetClicksRequest request)
        {
            var result = await _mediator.Send(request);

            return Json(result);
        }
    }
}