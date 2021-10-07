using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Implementation.ShortLinks.Commands.CreateLink;
using UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink;
using UrlShortener.Application.Implementation.ShortLinks.Queries.GetLinks;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.WebUI.Controllers
{
    [ApiController]
    [Route("s/")]
    public class LinksController : Controller
    {
        private readonly IMediator _mediator;

        public LinksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(CreateLinkResponse), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ValidationProblemDetails))]
        [HttpPost]
        public async Task<IActionResult> CreateLink([FromQuery] CreateLinkRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Redirect)]
        [ProducesErrorResponseType(typeof(ValidationProblemDetails))]
        [HttpGet("{alias}")]
        public async Task<IActionResult> GetLink(string alias)
        {
            var result = await _mediator.Send(new GetLinkRequest(alias));

            if (result.IsFailure)
                return NotFound();

            var uri = new Uri(result.Value.Link, UriKind.Absolute).AbsoluteUri;

            return Redirect(uri);
        }

        [ProducesResponseType(typeof(PaginatedList<GetLinksResponse>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ValidationProblemDetails))]
        [HttpGet]
        public async Task<IActionResult> GetLinks([FromQuery] GetLinksRequest request)
        {
            var result = await _mediator.Send(request);

            return Json(result);
        }
    }
}