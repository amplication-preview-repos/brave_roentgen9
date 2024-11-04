using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;

namespace AuctionSystem.APIs;

public interface IBikesService
{
    /// <summary>
    /// Create one Bike
    /// </summary>
    public Task<Bike> CreateBike(BikeCreateInput bike);

    /// <summary>
    /// Delete one Bike
    /// </summary>
    public Task DeleteBike(BikeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Bikes
    /// </summary>
    public Task<List<Bike>> Bikes(BikeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Bike records
    /// </summary>
    public Task<MetadataDto> BikesMeta(BikeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Bike
    /// </summary>
    public Task<Bike> Bike(BikeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Bike
    /// </summary>
    public Task UpdateBike(BikeWhereUniqueInput uniqueId, BikeUpdateInput updateDto);
}
