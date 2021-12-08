using MicroService.Product.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Product.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Products> Products { get; }
    }
}
