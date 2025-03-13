using Microsoft.AspNetCore.Mvc;
using API.Models;
using Application.Features.Boutiques.Queries;
using Application.Features.Boutiques.Commands;
using MediatR;

namespace API.Controllers;

[ApiController]
[Route("api/boutiques")]
public class BoutiqueController : ControllerBase
{
    private readonly ISender _sender;

    public BoutiqueController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedBoutiques(CancellationToken cancellationToken)
    {
        var query = new GetFeaturedBoutiquesQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("boutiquesWithInventory")]
    public async Task<IActionResult> GetBoutiquesWithInventory(CancellationToken cancellationToken)
    {
        var query = new GetBoutiquesWithInventoryQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("SearchBoutiquesByName")]
    public async Task<IActionResult> SearchBoutiquesByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        var query = new SearchBoutiqueByNameQuery(name);

        var result = await _sender.Send(query, cancellationToken);
        // return list of boutiques
        return Ok(result.Value);

    }

    [HttpGet("SearchBoutiquesByLocation")]
    public async Task<IActionResult> SearchBoutiquesByLocation([FromQuery] string location, CancellationToken cancellationToken)
    {
        var query = new SearchBoutiqueByLocationQuery(location);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
    
    [HttpGet("SearchBoutiquesByCategory")]
    public async Task<IActionResult> SearchBoutiquesByCategory([FromQuery] string category, CancellationToken cancellationToken)
    {
        var query = new SearchBoutiquesByCategoryQuery(category);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("GetBoutiqueById")]
    public async Task<IActionResult> GetBoutiqueById(int id, CancellationToken cancellationToken)
    {
        var query = new GetBoutiqueQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost("CreateBoutique")]
    public async Task<IActionResult> CreateBoutique(BoutiqueRequest request, CancellationToken cancellationToken)
    {
        var command = new AddBoutiqueCommand(
            null,
            request.OwnerId, 
            request.Name, 
            request.ProfilePicture, 
            request.Followers, 
            request.Description, 
            request.Category, 
            request.Location, 
            request.Contact, 
            request.InstagramLink);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) {
            return BadRequest(result.Error);
        }
        // return the created boutique location and id
        return CreatedAtAction(nameof(GetBoutiqueQuery), new { id = result.Value }, result.Value);
    }

    [HttpPost("UpdateBoutique")]
    public async Task<IActionResult> UpdateBoutique(BoutiqueRequest request, CancellationToken cancellationToken)
    {
        if (request.Id == null) {
            return BadRequest("Id is required");
        }

        var command = new UpdateBoutiqueCommand(
            request.Id.Value, //returns the value of the current Nullable<T> object if the Nullable<T>.HasValue property is true. An exception is thrown if the Nullable<T>.HasValue property is false.
            request.OwnerId, 
            request.Name, 
            request.ProfilePicture, 
            request.Followers, 
            request.Description, 
            request.Category, 
            request.Location, 
            request.Contact, 
            request.InstagramLink);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
} 