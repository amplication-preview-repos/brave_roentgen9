using AuctionSystem.APIs.Dtos;
using AuctionSystem.Infrastructure.Models;

namespace AuctionSystem.APIs.Extensions;

public static class CategoriesExtensions
{
    public static Category ToDto(this CategoryDbModel model)
    {
        return new Category
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CategoryDbModel ToModel(
        this CategoryUpdateInput updateDto,
        CategoryWhereUniqueInput uniqueId
    )
    {
        var category = new CategoryDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            category.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            category.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return category;
    }
}
