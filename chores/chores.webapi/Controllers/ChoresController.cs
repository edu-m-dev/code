using chores.bl.ef;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chores.webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserChores(string userId)
    {
        var result = await _mediator.Send(new GetUserChoresQuery(userId));
        return Ok(result);
    }

    [HttpPost("user/{userId}")]
    public async Task<IActionResult> AddUserChore(string userId, [FromBody] Chore chore)
    {
        var result = await _mediator.Send(new AddUserChoreCommand(userId, chore));
        return Ok(result);
    }
}
