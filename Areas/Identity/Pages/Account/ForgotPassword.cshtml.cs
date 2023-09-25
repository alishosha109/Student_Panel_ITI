// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Student_Panel_ITI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Student_Panel_ITI.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        //private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<AppUser> userManager /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            //_emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {

            [Required , MaxLength(50)]
            public string Username { get; set; }


            [Required]
            [DataType(DataType.Password)]
            [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z\\d]).{6,}$", ErrorMessage = "Password must have at least one digit, uppercase char, unique char, and length minimum 6")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Input.Username); //search his username

                if (user == null) //if not found
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    //return RedirectToPage("./ForgotPasswordConfirmation");

                    ModelState.AddModelError(string.Empty, "Invalid Username!");
                    return Page();
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ResetPassword",
                //    pageHandler: null,
                //    values: new { area = "Identity", code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    Input.Username,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user); //generate reset token provider for the new pass
                await _userManager.ResetPasswordAsync(user , resetToken , Input.Password); //reset his pass
                await _userManager.UpdateAsync(user); //update it in databse
                return RedirectToPage("./Login"); 
            }

            return Page();
        }
    }
}
