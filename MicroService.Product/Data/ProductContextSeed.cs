using MicroService.Product.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Product.Data
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Products> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Products> GetConfigureProducts()
        {
            return new List<Products>
            {
                new Products
                {
                    Name = "Takım Elbise",
                    Description = "ClebraCodeClebraCodeClebraCodeClebraCodeClebraCodeClebraCodeClebraCodeClebraCodeClebraCodeClebraCode"

                }
            };
        }
    }
}
