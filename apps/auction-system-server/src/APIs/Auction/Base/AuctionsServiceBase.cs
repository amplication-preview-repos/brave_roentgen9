using AuctionSystem.APIs;
using AuctionSystem.APIs.Common;
using AuctionSystem.APIs.Dtos;
using AuctionSystem.APIs.Errors;
using AuctionSystem.APIs.Extensions;
using AuctionSystem.Infrastructure;
using AuctionSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionSystem.APIs;

public abstract class AuctionsServiceBase : IAuctionsService
{
    protected readonly AuctionSystemDbContext _context;

    public AuctionsServiceBase(AuctionSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Auction
    /// </summary>
    public async Task<Auction> CreateAuction(AuctionCreateInput createDto)
    {
        var auction = new AuctionDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            auction.Id = createDto.Id;
        }

        _context.Auctions.Add(auction);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AuctionDbModel>(auction.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Auction
    /// </summary>
    public async Task DeleteAuction(AuctionWhereUniqueInput uniqueId)
    {
        var auction = await _context.Auctions.FindAsync(uniqueId.Id);
        if (auction == null)
        {
            throw new NotFoundException();
        }

        _context.Auctions.Remove(auction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Auctions
    /// </summary>
    public async Task<List<Auction>> Auctions(AuctionFindManyArgs findManyArgs)
    {
        var auctions = await _context
            .Auctions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return auctions.ConvertAll(auction => auction.ToDto());
    }

    /// <summary>
    /// Meta data about Auction records
    /// </summary>
    public async Task<MetadataDto> AuctionsMeta(AuctionFindManyArgs findManyArgs)
    {
        var count = await _context.Auctions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Auction
    /// </summary>
    public async Task<Auction> Auction(AuctionWhereUniqueInput uniqueId)
    {
        var auctions = await this.Auctions(
            new AuctionFindManyArgs { Where = new AuctionWhereInput { Id = uniqueId.Id } }
        );
        var auction = auctions.FirstOrDefault();
        if (auction == null)
        {
            throw new NotFoundException();
        }

        return auction;
    }

    /// <summary>
    /// Update one Auction
    /// </summary>
    public async Task UpdateAuction(AuctionWhereUniqueInput uniqueId, AuctionUpdateInput updateDto)
    {
        var auction = updateDto.ToModel(uniqueId);

        _context.Entry(auction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Auctions.Any(e => e.Id == auction.Id))
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
