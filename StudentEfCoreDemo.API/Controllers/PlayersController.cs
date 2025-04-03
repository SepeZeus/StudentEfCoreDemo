using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Players.Commands;
using StudentEfCoreDemo.Application.Features.Players.Queries;

namespace StudentEfCoreDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
        {
            var query = new GetPlayersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerById(int id)
        {
            var query = new GetPlayerByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(CreatePlayerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPlayerById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, UpdatePlayerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var command = new DeletePlayerCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
