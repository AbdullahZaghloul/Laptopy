using Laptopy.DTOS.Response;
using Laptopy.Repository.IRepository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Linq;
using System.Threading.Tasks;

namespace Laptopy.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct(int? page=1 , int?pageSize = 10)
        {
            var products =await productRepository.GetAsync(includes: [productRepository=>productRepository.ProductImages]);
            products = products.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            var productsResponse = products.Adapt<IEnumerable<ProductResponse>>();
            if(products is not null)
                return Ok(productsResponse);
            return BadRequest();
        }
        [HttpGet("GetProductsByName")]
        public async Task<IActionResult> GetProductsByName(string Name)
        {
            var products = await productRepository.GetAsync(expression: p => p.Name == Name, includes: [productRepository=>productRepository.ProductImages]);
            if(products is not null)
            {
                return Ok(products.Adapt<IEnumerable<ProductResponse>>());
            }
            return BadRequest();
        }
    }
}
