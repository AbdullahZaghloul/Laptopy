using System.Text.Json.Serialization;

namespace Laptopy.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Rating { get; set; }
        public List<ProductImages> ProductImages { get; set; } = [];
        public string Model { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;

    }
}
