using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[ApiController()]
public class CategoriesController : CategoriesControllerBase
{
    public CategoriesController(ICategoriesService service)
        : base(service) { }
}
