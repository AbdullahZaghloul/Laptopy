using Laptopy.DTOS.Response;
using Laptopy.Repository.IRepository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Laptopy.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(ICategoryRepository categoryRepository,IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet("GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(int CategoryId)
        {
            var Product = await _productRepository.GetAsync(expression: p => p.CategoryID == CategoryId, includes:[propa=>propa.ProductImages]);
            if(Product is not null)
            {
                return Ok(Product.Adapt<IEnumerable<ProductResponse>>());
            }
            return BadRequest();

        }
        [HttpGet("GetProductByPrice")]
        public async Task<IActionResult> GetProductByPrice(int minPrice, int maxPrice)
        {
            var products =await _productRepository.GetAsync(expression: p => p.Price >= minPrice && p.Price <= maxPrice);
            if(products is not null)
            {
                return Ok(products.Adapt<IEnumerable<ProductResponse>>());
            }
            return BadRequest();
        }
        [HttpGet("GetCategoryByRating")]
        public async Task<IActionResult> GetCategoryByRating(double Rating)
        {
            var products = await _productRepository.GetAsync(expression: p => p.Rating == Rating, includes: [p => p.ProductImages]);
            if(products is not null)
            {
                return Ok(products.Adapt<IEnumerable<ProductResponse>>());
            }
            return BadRequest();
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int ProductId)
        {
            var product =await _productRepository.GetOneAsync(expression: p => p.Id == ProductId, includes: [p=>p.ProductImages]);
            if(product is not null)
            {
                return Ok(product.Adapt<ProductResponse>());
            }
            return BadRequest();
        }
    }
}
