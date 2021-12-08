using MicroService.Product.Data.Interfaces;
using MicroService.Product.Entities;
using MicroService.Product.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Product.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Products>(settings.CollectionName);
            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Products> Products { get; }
    }
}
