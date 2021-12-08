using MicroService.Product.Entities;
using MicroService.Product.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MicroService.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Variables

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        #endregion

        #region Constructor
        public ProductController(IProductRepository productRepository ,ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        #endregion

        #region Crud_Actions

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Products>>> GetAll()
        {
            var products = await _productRepository.GetAllProductAsync();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name ="GetById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> GetById(string id)
        {
            var products = await _productRepository.GetProductAsync(id);
            if (products == null)
            {
                _logger.LogError($"Product with id:{id},hasn't been found in database");
                return NotFound();

            }
            return Ok(products);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Products),(int) HttpStatusCode.Created)]
        public async Task<ActionResult<Products>> AddProducts([FromBody] Products products)
        {
            await _productRepository.AddAsync(products);
            return CreatedAtRoute("GetById", new { id = products.Id }, products);
        }
            
        [HttpPut("[action]")]
        [ProducesResponseType(typeof(Products),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Products products)
        {
            return Ok(await _productRepository.UpdateAsync(products));
        }

        [HttpDelete("{id:length(24)}", Name ="Delete")]
        [ProducesResponseType(typeof(Products),(int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _productRepository.DeleteAsync(id));
        }
        #endregion



    }
}
