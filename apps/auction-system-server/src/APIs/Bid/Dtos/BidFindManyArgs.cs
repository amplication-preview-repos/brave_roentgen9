using AuctionSystem.APIs.Common;
using AuctionSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class BidFindManyArgs : FindManyInput<Bid, BidWhereInput> { }
