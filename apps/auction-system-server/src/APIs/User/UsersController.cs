using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
