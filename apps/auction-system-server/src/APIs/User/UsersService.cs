using AuctionSystem.Infrastructure;

namespace AuctionSystem.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(AuctionSystemDbContext context)
        : base(context) { }
}
