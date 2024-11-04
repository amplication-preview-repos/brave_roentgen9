using AuctionSystem.Infrastructure;

namespace AuctionSystem.APIs;

public class CategoriesService : CategoriesServiceBase
{
    public CategoriesService(AuctionSystemDbContext context)
        : base(context) { }
}
