using System.ComponentModel.DataAnnotations;

namespace Laptopy.Models.ViewModels
{
    public class ResetPasswordVM
    {
        
        [Required]
        public string Id { get; set; }

        [Required]
        public string token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
        

    }
}
