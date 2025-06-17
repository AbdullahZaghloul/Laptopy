using Laptopy.DTOS.Response;
using Laptopy.DTOS.Request;
using Laptopy.Models;
using Mapster;

namespace Laptopy.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductResponse>();
            config.NewConfig<Category, CategoryResponse>();
            config.NewConfig<ContactUsRequest, ContactUs>();
        }
    }
}
