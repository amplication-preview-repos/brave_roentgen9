using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;

namespace AuctionSystem.APIs;

public interface IAuctionsService
{
    /// <summary>
    /// Create one Auction
    /// </summary>
    public Task<Auction> CreateAuction(AuctionCreateInput auction);

    /// <summary>
    /// Delete one Auction
    /// </summary>
    public Task DeleteAuction(AuctionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Auctions
    /// </summary>
    public Task<List<Auction>> Auctions(AuctionFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Auction records
    /// </summary>
    public Task<MetadataDto> AuctionsMeta(AuctionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Auction
    /// </summary>
    public Task<Auction> Auction(AuctionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Auction
    /// </summary>
    public Task UpdateAuction(AuctionWhereUniqueInput uniqueId, AuctionUpdateInput updateDto);
}
