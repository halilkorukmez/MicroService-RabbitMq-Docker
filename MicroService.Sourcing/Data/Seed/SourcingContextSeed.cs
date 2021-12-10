using MicroService.Sourcing.Entities;
using MicroService.Sourcing.Entities.Enum;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Data.Seed
{
    public class SourcingContextSeed
    {
        public static void SeedData(IMongoCollection<Auction> auctionCollection)
        {
            bool exist = auctionCollection.Find(p => true).Any();
            if (!exist)
            {
                auctionCollection.InsertManyAsync(GetPreconfiguredAuctions());
            }
        }

        private static IEnumerable<Auction> GetPreconfiguredAuctions()
        {
            return new List<Auction>()
            {
                new Auction()
                {
                    Name = "ClebraCode.Halil",
                    Description = "ClebraCode.Halil",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(1),
                    IncludedSellers = new List<string>()
                    {
                        "clebra@code.com",
                        "clebra@code.com",
                        "clebra@code.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active
                }
            };
        }
    }
}
