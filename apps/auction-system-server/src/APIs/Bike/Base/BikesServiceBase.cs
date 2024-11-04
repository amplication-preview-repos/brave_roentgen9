using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using AuctionSystem.APIs.Extensions;
using AuctionSystem.Infrastructure;
using AuctionSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionSystem.APIs;

public abstract class BikesServiceBase : IBikesService
{
    protected readonly AuctionSystemDbContext _context;

    public BikesServiceBase(AuctionSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Bike
    /// </summary>
    public async Task<Bike> CreateBike(BikeCreateInput createDto)
    {
        var bike = new BikeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            bike.Id = createDto.Id;
        }

        _context.Bikes.Add(bike);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BikeDbModel>(bike.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Bike
    /// </summary>
    public async Task DeleteBike(BikeWhereUniqueInput uniqueId)
    {
        var bike = await _context.Bikes.FindAsync(uniqueId.Id);
        if (bike == null)
        {
            throw new NotFoundException();
        }

        _context.Bikes.Remove(bike);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Bikes
    /// </summary>
    public async Task<List<Bike>> Bikes(BikeFindManyArgs findManyArgs)
    {
        var bikes = await _context
            .Bikes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return bikes.ConvertAll(bike => bike.ToDto());
    }

    /// <summary>
    /// Meta data about Bike records
    /// </summary>
    public async Task<MetadataDto> BikesMeta(BikeFindManyArgs findManyArgs)
    {
        var count = await _context.Bikes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Bike
    /// </summary>
    public async Task<Bike> Bike(BikeWhereUniqueInput uniqueId)
    {
        var bikes = await this.Bikes(
            new BikeFindManyArgs { Where = new BikeWhereInput { Id = uniqueId.Id } }
        );
        var bike = bikes.FirstOrDefault();
        if (bike == null)
        {
            throw new NotFoundException();
        }

        return bike;
    }

    /// <summary>
    /// Update one Bike
    /// </summary>
    public async Task UpdateBike(BikeWhereUniqueInput uniqueId, BikeUpdateInput updateDto)
    {
        var bike = updateDto.ToModel(uniqueId);

        _context.Entry(bike).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Bikes.Any(e => e.Id == bike.Id))
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
