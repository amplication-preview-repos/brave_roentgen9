using AuctionSystem.APIs.Common;
using AuctionSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class BikeFindManyArgs : FindManyInput<Bike, BikeWhereInput> { }
