using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Implementation.LinkClicks.Queries.GetClicksStatistic;

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

        [HttpGet]
        public async Task<IActionResult> GetClicks(int pageIndex = 0, int pageSize = 20)
        {
            var result = await _mediator.Send(new GetClicksRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(result);
        }
    }
}