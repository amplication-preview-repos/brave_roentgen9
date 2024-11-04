using AuctionSystem.APIs.Dtos;
using AuctionSystem.Infrastructure.Models;

namespace AuctionSystem.APIs.Extensions;

public static class AuctionsExtensions
{
    public static Auction ToDto(this AuctionDbModel model)
    {
        return new Auction
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AuctionDbModel ToModel(
        this AuctionUpdateInput updateDto,
        AuctionWhereUniqueInput uniqueId
    )
    {
        var auction = new AuctionDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            auction.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            auction.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return auction;
    }
}
