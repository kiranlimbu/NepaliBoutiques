using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Inventories.Queries;
using Application.Features.Inventories.Commands;
using API.Models;
using Application.Features.Boutiques.Queries;
using Application.Features.Inventories.Models;

namespace API.Controllers;

/// <summary>
/// Controller for managing inventory items
/// </summary>
[ApiController]
[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly ISender _sender;

    public InventoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("GetInventoryForBoutique")]
    public async Task<IActionResult> GetInventoryForBoutique(int boutiqueId, int offset, int limit, CancellationToken cancellationToken)
    {
        var query = new GetInventoryForBoutiqueQuery(boutiqueId, offset, limit);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    /// <summary>
    /// Adds new inventory items to a boutique. handles both single and multiple items.
    /// </summary>
    /// <param name="request">The inventory item details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>IDs of the created inventory items</returns>
    /// <response code="201">Returns the IDs of the created inventory items</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the boutique is not found</response>
    [HttpPost("AddInventoryItems")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddInventoryItems(List<InventoryItemModel> request, CancellationToken cancellationToken)
    {

        var command = new AddInventoryItemsCommand(request);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        // return the created inventory items location for the boutique
        return CreatedAtAction(nameof(GetInventoryForBoutique), new { boutiqueId = request[0].BoutiqueId }, result.Value);
    }

    [HttpPut("UpdateInventoryItem")]
    public async Task<IActionResult> UpdateInventoryItem(InventoryItemModel request, CancellationToken cancellationToken)
    {
        var command = new UpdateInventoryItemCommand(request);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return NoContent();
    }
    

}
