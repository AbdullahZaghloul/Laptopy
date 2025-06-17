using Laptopy.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Laptopy.DTOS.Request;
using Mapster;
using Laptopy.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace Laptopy.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsController(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        [HttpPost("ContactUs")]
        public async Task<IActionResult> ContactUs(ContactUsRequest contactUsRequest)
        {
            var contactUs = contactUsRequest.Adapt<ContactUs>();
            var Result = await _contactUsRepository.CreateAsync(contactUs);
            await _contactUsRepository.CommitAsync();
            if(Result is not null)
            {
                return Ok("added successfully");
            }
            return BadRequest();
        }
    }
}
