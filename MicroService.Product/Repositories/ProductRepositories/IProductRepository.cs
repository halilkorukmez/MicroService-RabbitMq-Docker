using MicroService.Product.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroService.Product.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProductAsync();
        Task<Products> GetProductAsync(string id);
        Task<IEnumerable<Products>> GetProductByName(string name);

        Task AddAsync(Products products);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpdateAsync(Products products);

    }
}
