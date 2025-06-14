using Laptopy.Models;
using Laptopy.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Laptopy.Areas.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> signIn;

        public AccountController(UserManager<ApplicationUser> userManager, IEmailSender emailSender,SignInManager<ApplicationUser> signIn)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            this.signIn = signIn;
        }

        [HttpPost("RegisterAccount")]
        public async Task<IActionResult> RegisterAccount(RegisterVM registerVM) 
        {
            var ApplicationUser = new ApplicationUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };
            var result = await _userManager.CreateAsync(ApplicationUser,registerVM.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(ApplicationUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", 
                    new { area = "Identity", ApplicationUser.Id, token }, Request.Scheme);
                await _emailSender.SendEmailAsync(ApplicationUser.Email, "Confirmation Email", $"<h1>Confirm Your Account By Click <a href='{confirmationLink}'>Here</a></h1>");
                return Ok("Registered Successfully");

            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string Id , string token)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user is not null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return Ok("email confirmed successfully");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var applicationUser = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
            if (applicationUser is null)
            {
                applicationUser =await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);

            }
            if(applicationUser is not null)
            {
                var result = await _userManager.CheckPasswordAsync(applicationUser, loginVM.Password);
                if (result)
                {
                    await signIn.SignInAsync(applicationUser, true);
                    return Ok("sign in sucessfully");
                }
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            var applicationUser = await _userManager.FindByEmailAsync(forgetPasswordVM.UserNameOrEmail);
            if(applicationUser is null)
            {
                applicationUser =await _userManager.FindByNameAsync(forgetPasswordVM.UserNameOrEmail);
            }
            if(applicationUser is not null)
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
                var resetPassword = Url.Action("ResetPassword", "Account", new { area = "Identity", applicationUser.Id, token },Request.Scheme);
                await _emailSender.SendEmailAsync(applicationUser.Email, "Reset Password", $"<h1>Reset Password Account By Click <a href='{resetPassword}'>Here</a></h1>");
                return Ok("the email send successfully");
            }
            return BadRequest();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordVM model)
        {

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.token, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest(result.Errors.Select(e => e.Description));
        }

    }

}
