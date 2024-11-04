using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using AuctionSystem.APIs.Extensions;
using AuctionSystem.Infrastructure;
using AuctionSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionSystem.APIs;

public abstract class CategoriesServiceBase : ICategoriesService
{
    protected readonly AuctionSystemDbContext _context;

    public CategoriesServiceBase(AuctionSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Category
    /// </summary>
    public async Task<Category> CreateCategory(CategoryCreateInput createDto)
    {
        var category = new CategoryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            category.Id = createDto.Id;
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CategoryDbModel>(category.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Category
    /// </summary>
    public async Task DeleteCategory(CategoryWhereUniqueInput uniqueId)
    {
        var category = await _context.Categories.FindAsync(uniqueId.Id);
        if (category == null)
        {
            throw new NotFoundException();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Categories
    /// </summary>
    public async Task<List<Category>> Categories(CategoryFindManyArgs findManyArgs)
    {
        var categories = await _context
            .Categories.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return categories.ConvertAll(category => category.ToDto());
    }

    /// <summary>
    /// Meta data about Category records
    /// </summary>
    public async Task<MetadataDto> CategoriesMeta(CategoryFindManyArgs findManyArgs)
    {
        var count = await _context.Categories.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Category
    /// </summary>
    public async Task<Category> Category(CategoryWhereUniqueInput uniqueId)
    {
        var categories = await this.Categories(
            new CategoryFindManyArgs { Where = new CategoryWhereInput { Id = uniqueId.Id } }
        );
        var category = categories.FirstOrDefault();
        if (category == null)
        {
            throw new NotFoundException();
        }

        return category;
    }

    /// <summary>
    /// Update one Category
    /// </summary>
    public async Task UpdateCategory(
        CategoryWhereUniqueInput uniqueId,
        CategoryUpdateInput updateDto
    )
    {
        var category = updateDto.ToModel(uniqueId);

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Categories.Any(e => e.Id == category.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
