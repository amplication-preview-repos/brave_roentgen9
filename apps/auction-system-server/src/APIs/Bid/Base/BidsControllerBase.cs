using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BidsControllerBase : ControllerBase
{
    protected readonly IBidsService _service;

    public BidsControllerBase(IBidsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Bid
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Bid>> CreateBid(BidCreateInput input)
    {
        var bid = await _service.CreateBid(input);

        return CreatedAtAction(nameof(Bid), new { id = bid.Id }, bid);
    }

    /// <summary>
    /// Delete one Bid
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteBid([FromRoute()] BidWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBid(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Bids
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Bid>>> Bids([FromQuery()] BidFindManyArgs filter)
    {
        return Ok(await _service.Bids(filter));
    }

    /// <summary>
    /// Meta data about Bid records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BidsMeta([FromQuery()] BidFindManyArgs filter)
    {
        return Ok(await _service.BidsMeta(filter));
    }

    /// <summary>
    /// Get one Bid
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Bid>> Bid([FromRoute()] BidWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Bid(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Bid
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateBid(
        [FromRoute()] BidWhereUniqueInput uniqueId,
        [FromQuery()] BidUpdateInput bidUpdateDto
    )
    {
        try
        {
            await _service.UpdateBid(uniqueId, bidUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
