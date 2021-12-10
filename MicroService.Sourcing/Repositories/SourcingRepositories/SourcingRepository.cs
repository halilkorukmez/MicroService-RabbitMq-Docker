using MicroService.Sourcing.Data.Interface;
using MicroService.Sourcing.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Repositories.SourcingRepositories
{
    public class SourcingRepository : ISourcingRepository
    {
        private readonly ISourcingContext _context;

        public SourcingRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Auction auction)
        {
           await _context.Auctions.InsertOneAsync(auction);
            
        }

        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(s => s.Id, id);
            DeleteResult deleteResult = await _context.Auctions.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Auction> GetAsync(string id)
        {
            return await _context.Auctions.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Auction> GetByName(string name)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(s => s.Name, name);
            return await _context.Auctions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Auction>> GetListAsync()
        {
            return await _context.Auctions.Find(s => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Auction auction)
        {
            var updateResult = await _context.Auctions.ReplaceOneAsync(s => s.Id.Equals(auction.Id),auction);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
