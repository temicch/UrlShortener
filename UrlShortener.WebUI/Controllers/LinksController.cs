using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Implementation.ShortLinks.Commands.CreateLink;
using UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink;

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

        [HttpPost("{longUrl}")]
        public async Task<IActionResult> CreateLink(string longUrl, string alias = null)
        {
            var result = await _mediator.Send(new CreateLinkRequest(longUrl, alias));

            return Ok(result);
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> GetLink(string shortUrl)
        {
            var result = await _mediator.Send(new GetLinkRequest(shortUrl));

            if (result == null)
                return NoContent();

            var uri = new Uri(result.Link, UriKind.Absolute).AbsoluteUri;

            return Redirect(uri);
        }
    }
}