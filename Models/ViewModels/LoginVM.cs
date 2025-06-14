using System.ComponentModel.DataAnnotations;

namespace Laptopy.Models.ViewModels
{
    public class LoginVM
    {
        [Length(1,100)]
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
