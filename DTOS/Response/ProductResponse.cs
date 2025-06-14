using Laptopy.Models;

namespace Laptopy.DTOS.Response
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public List<ProductImages> ProductImages { get; set; } = [];
        public string Model { get; set; }
    }
}
