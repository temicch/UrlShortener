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

        /// <summary>
        ///     Create link for specified URL address.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         You must encode 'Link' parameter before submitting it otherwise application
        ///         will not properly support URLs containing symbols such as hash,
        ///         semicolon, plus and ampersand (among others).
        ///     </para>
        ///     <para>
        ///         You can specify the 'SuggestedAlias' parameter if you'd like to pick a shortened
        ///         URL instead of having randomly generate one. These must be between 3 and 30
        ///         characters long and can only contain alphanumeric characters and underscores.
        ///         Shortened URLs are case sensitive.
        ///     </para>
        /// </remarks>
        /// <response code="200">Returns the newly created link</response>
        /// <response code="400">Returns validation or another problem</response>
        [ProducesResponseType(typeof(CreateLinkResponse), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ValidationProblemDetails))]
        [HttpPost]
        public async Task<IActionResult> CreateLink([FromQuery] CreateLinkRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }

        /// <summary>
        ///     Redirects to URL with specified <paramref name="alias" />
        /// </summary>
        /// <response code="302">Redirect to URL address</response>
        /// <response code="404">Link with specified Alias not found</response>
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

        /// <summary>
        ///     Get all app generated links
        /// </summary>
        /// <response code="200">Returns all application links</response>
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