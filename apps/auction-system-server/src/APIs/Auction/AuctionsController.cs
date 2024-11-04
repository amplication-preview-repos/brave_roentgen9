using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[ApiController()]
public class AuctionsController : AuctionsControllerBase
{
    public AuctionsController(IAuctionsService service)
        : base(service) { }
}
