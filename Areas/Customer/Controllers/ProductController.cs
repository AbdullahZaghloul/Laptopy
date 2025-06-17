using Laptopy.DTOS.Response;
using Laptopy.Models;
using Laptopy.Repository.IRepository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;
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

        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(string? categoryName,int? minPrice,int? maxPrice ,double? Rating,int? page=1,int?pageSize =10)
        {
            Expression<Func<Product, bool>>? filter = p =>
            (string.IsNullOrEmpty(categoryName) || p.Category.Name == categoryName) &&
            (!minPrice.HasValue || p.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || p.Price <= maxPrice.Value) &&
            (!Rating.HasValue || p.Rating >= Rating.Value);


            var products = await _productRepository.GetAsync(expression: filter, includes:[p=>p.ProductImages]);

            products = products.Skip((page.Value-1)*pageSize.Value).Take(pageSize.Value);

            if (products is not null)
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
