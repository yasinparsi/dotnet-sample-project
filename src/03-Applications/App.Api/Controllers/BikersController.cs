using Mediator;
using Microsoft.AspNetCore.Mvc;
using SnappFood.DotNetSampleProject.App.Api.Models;
using SnappFood.DotNetSampleProject.Core.ApplicationServices.Commands;
using SnappFood.DotNetSampleProject.Core.ApplicationServices.Queries;

namespace SnappFood.DotNetSampleProject.App.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class BikerController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] IMediator mediator, CancellationToken ct)
    {
        GetBikersQuery query = new();
        var rs = await mediator.Send(query, ct);

        return Ok(rs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromServices] IMediator mediator, [FromRoute] long id, CancellationToken ct)
    {
        GetBikerByIdQuery query = new()
        {
            Id = id,
        };
        var rs = await mediator.Send(query, ct);

        return Ok(rs);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromServices] IMediator mediator,
        [FromBody] RegisterBikerDto dto, CancellationToken ct)
    {
        RegisterBikerCommand command = new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };
        var rs = await mediator.Send(command, ct);

        var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/biker/{rs}";
        return Created(url, null);
    }
}
