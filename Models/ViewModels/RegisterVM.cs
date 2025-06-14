using Microsoft.Build.Framework;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace Laptopy.Models.ViewModels
{
    public class RegisterVM
    {
        [Length(1,100)]
        public string UserName { get; set; }

        [Length(1,100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
    }
}
