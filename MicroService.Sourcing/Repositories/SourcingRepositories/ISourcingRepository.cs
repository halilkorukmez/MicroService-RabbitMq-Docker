using MicroService.Sourcing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Repositories.SourcingRepositories
{
    public interface ISourcingRepository
    {
        Task<IEnumerable<Auction>> GetListAsync();
        Task<Auction> GetAsync(string id);
        Task<Auction> GetByName(string name);
        Task AddAsync(Auction auction);
        Task<bool> UpdateAsync(Auction auction);
        Task<bool> DeleteAsync(string id);

    }
}
