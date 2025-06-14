using Laptopy.Models;

namespace Laptopy.DTOS.Response
{
    public class CategoryResponse
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
    }
}
