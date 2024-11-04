using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;

namespace AuctionSystem.APIs;

public interface ICategoriesService
{
    /// <summary>
    /// Create one Category
    /// </summary>
    public Task<Category> CreateCategory(CategoryCreateInput category);

    /// <summary>
    /// Delete one Category
    /// </summary>
    public Task DeleteCategory(CategoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Categories
    /// </summary>
    public Task<List<Category>> Categories(CategoryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Category records
    /// </summary>
    public Task<MetadataDto> CategoriesMeta(CategoryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Category
    /// </summary>
    public Task<Category> Category(CategoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Category
    /// </summary>
    public Task UpdateCategory(CategoryWhereUniqueInput uniqueId, CategoryUpdateInput updateDto);
}
