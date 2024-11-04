using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AuctionsControllerBase : ControllerBase
{
    protected readonly IAuctionsService _service;

    public AuctionsControllerBase(IAuctionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Auction
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Auction>> CreateAuction(AuctionCreateInput input)
    {
        var auction = await _service.CreateAuction(input);

        return CreatedAtAction(nameof(Auction), new { id = auction.Id }, auction);
    }

    /// <summary>
    /// Delete one Auction
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAuction([FromRoute()] AuctionWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAuction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Auctions
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Auction>>> Auctions(
        [FromQuery()] AuctionFindManyArgs filter
    )
    {
        return Ok(await _service.Auctions(filter));
    }

    /// <summary>
    /// Meta data about Auction records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AuctionsMeta(
        [FromQuery()] AuctionFindManyArgs filter
    )
    {
        return Ok(await _service.AuctionsMeta(filter));
    }

    /// <summary>
    /// Get one Auction
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Auction>> Auction([FromRoute()] AuctionWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Auction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Auction
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAuction(
        [FromRoute()] AuctionWhereUniqueInput uniqueId,
        [FromQuery()] AuctionUpdateInput auctionUpdateDto
    )
    {
        try
        {
            await _service.UpdateAuction(uniqueId, auctionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
