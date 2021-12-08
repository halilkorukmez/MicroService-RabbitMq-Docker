using MicroService.Product.Data.Interfaces;
using MicroService.Product.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroService.Product.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Products products)
        {
            await _context.Products.InsertOneAsync(products);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Products>.Filter.Eq(m => m.Id , id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Products>> GetAllProductAsync()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Products> GetProductAsync(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Products>> GetProductByName(string name)
        {
            var filter = Builders<Products>.Filter.ElemMatch(p => p.Name , name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Products products)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == products.Id, replacement: products);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; 
        }
    }
}
