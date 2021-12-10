using MicroService.Sourcing.Data.Interface;
using MicroService.Sourcing.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Repositories.BidRepositories
{
    public class BidRepository : IBidRepository
    {

        private ISourcingContext _context;
        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }


        public async Task SendAsync(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }

        public async Task<List<Bid>> GetByAuctionIdAsync(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(b => b.AuctionId, id);
            List<Bid> bids = await _context.Bids.Find(filter).ToListAsync();

            bids = bids.OrderByDescending(b => b.CreatedAt)
                .GroupBy(b => b.SellerUserName)
                .Select(b => new Bid 
                {
                    AuctionId = b.FirstOrDefault().AuctionId,
                    Price = b.FirstOrDefault().Price,
                    CreatedAt = b.FirstOrDefault().CreatedAt,
                    SellerUserName = b.FirstOrDefault().SellerUserName,
                    ProductId = b.FirstOrDefault().ProductId,
                    Id = b.FirstOrDefault().Id

                }).ToList();
            return bids; 
        }

        public async Task<Bid> GetWinnerIdAsync(string id)
        {
            List<Bid> bids = await GetByAuctionIdAsync(id);
            return bids.OrderByDescending(b => b.Price).FirstOrDefault();
        }
    }
}
