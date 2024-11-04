using AuctionSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionSystem.Infrastructure;

public class AuctionSystemDbContext : DbContext
{
    public AuctionSystemDbContext(DbContextOptions<AuctionSystemDbContext> options)
        : base(options) { }

    public DbSet<BikeDbModel> Bikes { get; set; }

    public DbSet<BidDbModel> Bids { get; set; }

    public DbSet<CategoryDbModel> Categories { get; set; }

    public DbSet<AuctionDbModel> Auctions { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
