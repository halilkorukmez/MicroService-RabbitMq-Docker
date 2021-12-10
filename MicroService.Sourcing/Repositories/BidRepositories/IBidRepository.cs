using MicroService.Sourcing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Repositories.BidRepositories
{
    public interface IBidRepository
    {
        Task SendAsync(Bid bid);
        Task<List<Bid>> GetByAuctionIdAsync(string id);
        Task<Bid> GetWinnerIdAsync(string id);
    }
}
