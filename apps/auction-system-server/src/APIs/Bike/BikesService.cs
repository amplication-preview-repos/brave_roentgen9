using AuctionSystem.Infrastructure;

namespace AuctionSystem.APIs;

public class BikesService : BikesServiceBase
{
    public BikesService(AuctionSystemDbContext context)
        : base(context) { }
}
