using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubastaYa.Aplication.UseCases.Auctions.Commands.CreateCommand;
using SubastaYa.Aplication.UseCases.Auctions.Commands.DeleteCommand;
using SubastaYa.Aplication.UseCases.Auctions.Commands.UpdateCommand;
using SubastaYa.Aplication.UseCases.Auctions.Queries.GetAllQuery;
using SubastaYa.Aplication.UseCases.Auctions.Queries.GetByIdQuery;

namespace SubastaYa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionController : ControllerBase
{
    private readonly ISender _sender;

    public AuctionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("auctions")]
    public async Task<IActionResult> GetAllAuctions()
    {
        var query = new GetAllAuctionQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("auctions/{id}")]
    public async Task<IActionResult> GetAuctionById(int id)
    {
        var query = new GetAuctionByIdQuery { Id = id };
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPost("auctions")]
    public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut("auctions/{id}")]
    public async Task<IActionResult> UpdateAuction(int id, [FromBody] UpdateAuctionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Auction ID mismatch.");
        }
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("auctions/{id}")]
    public async Task<IActionResult> DeleteAuction(int id)
    {
        var command = new DeleteAuctionCommand { Id = id };
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
