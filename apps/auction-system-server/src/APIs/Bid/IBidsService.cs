using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;

namespace AuctionSystem.APIs;

public interface IBidsService
{
    /// <summary>
    /// Create one Bid
    /// </summary>
    public Task<Bid> CreateBid(BidCreateInput bid);

    /// <summary>
    /// Delete one Bid
    /// </summary>
    public Task DeleteBid(BidWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Bids
    /// </summary>
    public Task<List<Bid>> Bids(BidFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Bid records
    /// </summary>
    public Task<MetadataDto> BidsMeta(BidFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Bid
    /// </summary>
    public Task<Bid> Bid(BidWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Bid
    /// </summary>
    public Task UpdateBid(BidWhereUniqueInput uniqueId, BidUpdateInput updateDto);
}
