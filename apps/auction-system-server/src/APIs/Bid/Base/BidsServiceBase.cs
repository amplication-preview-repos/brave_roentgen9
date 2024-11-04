using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using AuctionSystem.APIs.Extensions;
using AuctionSystem.Infrastructure;
using AuctionSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionSystem.APIs;

public abstract class BidsServiceBase : IBidsService
{
    protected readonly AuctionSystemDbContext _context;

    public BidsServiceBase(AuctionSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Bid
    /// </summary>
    public async Task<Bid> CreateBid(BidCreateInput createDto)
    {
        var bid = new BidDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            bid.Id = createDto.Id;
        }

        _context.Bids.Add(bid);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BidDbModel>(bid.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Bid
    /// </summary>
    public async Task DeleteBid(BidWhereUniqueInput uniqueId)
    {
        var bid = await _context.Bids.FindAsync(uniqueId.Id);
        if (bid == null)
        {
            throw new NotFoundException();
        }

        _context.Bids.Remove(bid);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Bids
    /// </summary>
    public async Task<List<Bid>> Bids(BidFindManyArgs findManyArgs)
    {
        var bids = await _context
            .Bids.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return bids.ConvertAll(bid => bid.ToDto());
    }

    /// <summary>
    /// Meta data about Bid records
    /// </summary>
    public async Task<MetadataDto> BidsMeta(BidFindManyArgs findManyArgs)
    {
        var count = await _context.Bids.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Bid
    /// </summary>
    public async Task<Bid> Bid(BidWhereUniqueInput uniqueId)
    {
        var bids = await this.Bids(
            new BidFindManyArgs { Where = new BidWhereInput { Id = uniqueId.Id } }
        );
        var bid = bids.FirstOrDefault();
        if (bid == null)
        {
            throw new NotFoundException();
        }

        return bid;
    }

    /// <summary>
    /// Update one Bid
    /// </summary>
    public async Task UpdateBid(BidWhereUniqueInput uniqueId, BidUpdateInput updateDto)
    {
        var bid = updateDto.ToModel(uniqueId);

        _context.Entry(bid).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Bids.Any(e => e.Id == bid.Id))
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
