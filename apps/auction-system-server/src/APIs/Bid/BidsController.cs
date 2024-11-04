using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[ApiController()]
public class BidsController : BidsControllerBase
{
    public BidsController(IBidsService service)
        : base(service) { }
}
