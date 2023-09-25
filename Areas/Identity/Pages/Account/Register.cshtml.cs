// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Student_Panel_ITI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using Student_Panel_ITI.Repos;
using Student_Panel_ITI.Repos.Interfaces;

namespace Student_Panel_ITI.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IAdminRepository adminRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IInstructorRepository instructorRepository;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger, 
            IAdminRepository _adminRepository, 
            IStudentRepository _studentRepository, 
            IInstructorRepository _instructorRepository)
            //IEmailSender emailSender
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            adminRepository = _adminRepository;
            studentRepository = _studentRepository;
            instructorRepository = _instructorRepository;
            //_emailSender = emailSender;
        }


        [BindProperty]
        public InputModel Input { get; set; }


        public string ReturnUrl { get; set; }


        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public enum UserType { Admin, Instructor, Student }
        public class InputModel
        {
            public UserType userType { get; set; }



            [Required, MaxLength(50)]
            [DisplayName("Full Name")]
            public string FullName { get; set; }


            [Required , MaxLength(50)]
            public string Username { get; set; }

            
            [Required]
            [EmailAddress]
            [RegularExpression("^[a-zA-Z0-9._%+-]+@(gmail\\.com|yahoo\\.com|outlook\\.com)$")]
            public string Email { get; set; }


            [Required]
            [Phone , MaxLength(11)]
            [RegularExpression("^(011|012|010|015)\\d{8}$")]
            public string PhoneNumber { get; set; }


            [Required]
            [DataType(DataType.Password)]
            [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z\\d]).{6,}$", ErrorMessage = "Password must have at least one digit, uppercase char, unique char, and length minimum 6")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FullName = Input.FullName;
                user.PhoneNumber = Input.PhoneNumber;   
                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None); 
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);




                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    //var userId = await _userManager.GetUserIdAsync(user);
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Username, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Username, returnUrl = returnUrl });
                    //}
                    //else
                    //{

                    user.LockoutEnabled = false;

                    if (Input.userType == UserType.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (Input.userType == UserType.Instructor)
                    {
                        await _userManager.AddToRoleAsync(user, "Instructor");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Student");
                    }


                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        Admin newAdmin = new()
                        {
                            AspNetUserID = user.Id,
                        };

                        adminRepository.CreateAdmin(newAdmin);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false); //create cookie 

                    return LocalRedirect(returnUrl);

                    //}
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }



        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
    }
}
