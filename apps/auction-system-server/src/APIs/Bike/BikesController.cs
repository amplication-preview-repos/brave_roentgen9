using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[ApiController()]
public class BikesController : BikesControllerBase
{
    public BikesController(IBikesService service)
        : base(service) { }
}
