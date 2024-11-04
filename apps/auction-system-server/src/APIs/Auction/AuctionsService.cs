using AuctionSystem.Infrastructure;

namespace AuctionSystem.APIs;

public class AuctionsService : AuctionsServiceBase
{
    public AuctionsService(AuctionSystemDbContext context)
        : base(context) { }
}
