using AuctionSystem.APIs.Dtos;
using AuctionSystem.Infrastructure.Models;

namespace AuctionSystem.APIs.Extensions;

public static class BidsExtensions
{
    public static Bid ToDto(this BidDbModel model)
    {
        return new Bid
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BidDbModel ToModel(this BidUpdateInput updateDto, BidWhereUniqueInput uniqueId)
    {
        var bid = new BidDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            bid.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            bid.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return bid;
    }
}
