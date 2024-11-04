using AuctionSystem.APIs.Dtos;
using AuctionSystem.Infrastructure.Models;

namespace AuctionSystem.APIs.Extensions;

public static class BikesExtensions
{
    public static Bike ToDto(this BikeDbModel model)
    {
        return new Bike
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BikeDbModel ToModel(this BikeUpdateInput updateDto, BikeWhereUniqueInput uniqueId)
    {
        var bike = new BikeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            bike.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            bike.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return bike;
    }
}
