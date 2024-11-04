using AuctionSystem.Infrastructure;

namespace AuctionSystem.APIs;

public class BidsService : BidsServiceBase
{
    public BidsService(AuctionSystemDbContext context)
        : base(context) { }
}
