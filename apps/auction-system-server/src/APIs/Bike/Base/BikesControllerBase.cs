using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BikesControllerBase : ControllerBase
{
    protected readonly IBikesService _service;

    public BikesControllerBase(IBikesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Bike
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Bike>> CreateBike(BikeCreateInput input)
    {
        var bike = await _service.CreateBike(input);

        return CreatedAtAction(nameof(Bike), new { id = bike.Id }, bike);
    }

    /// <summary>
    /// Delete one Bike
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteBike([FromRoute()] BikeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBike(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Bikes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Bike>>> Bikes([FromQuery()] BikeFindManyArgs filter)
    {
        return Ok(await _service.Bikes(filter));
    }

    /// <summary>
    /// Meta data about Bike records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BikesMeta([FromQuery()] BikeFindManyArgs filter)
    {
        return Ok(await _service.BikesMeta(filter));
    }

    /// <summary>
    /// Get one Bike
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Bike>> Bike([FromRoute()] BikeWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Bike(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Bike
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateBike(
        [FromRoute()] BikeWhereUniqueInput uniqueId,
        [FromQuery()] BikeUpdateInput bikeUpdateDto
    )
    {
        try
        {
            await _service.UpdateBike(uniqueId, bikeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
