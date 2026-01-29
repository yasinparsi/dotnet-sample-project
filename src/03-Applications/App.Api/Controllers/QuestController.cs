using Mediator;
using Microsoft.AspNetCore.Mvc;
using SnappFood.DotNetSampleProject.App.Api.Models;
using SnappFood.DotNetSampleProject.Core.ApplicationServices.Commands;
using SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

namespace SnappFood.DotNetSampleProject.App.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class QuestController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromServices] IMediator mediator, [FromRoute] long id, CancellationToken ct)
    {
        GetQuestByIdQuery query = new()
        {
            Id = id,
        };
        var rs = await mediator.Send(query, ct);

        return Ok(rs);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] IMediator mediator, CancellationToken ct)
    {
        GetQuestsQuery query = new();
        var rs = await mediator.Send(query, ct);
        return Ok(rs);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromServices] IMediator mediator,
        [FromBody] CreateQuestDto dto, CancellationToken ct)
    {
        CreateQuestCommand command = new()
        {
            Name = dto.Name,
            Incentive = dto.Incentive,
        };
        var rs = await mediator.Send(command, ct);

        var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/quest/{rs}";
        return Created(url, null);
    }
}
