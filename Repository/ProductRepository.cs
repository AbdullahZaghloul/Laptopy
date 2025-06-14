using Laptopy.Data;
using Laptopy.Models;

namespace Laptopy.Repository.IRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
