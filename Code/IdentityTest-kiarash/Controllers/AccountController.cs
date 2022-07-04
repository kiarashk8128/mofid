using IdentityTest.Models.Dto;
using IdentityTest.Models.Dto.Account;
using IdentityTest.Models.Entities;
using IdentityTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IdentityTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailService _emailService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = new EmailService();
        }
        [Authorize]
        public IActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            AccountInfoDto myAccount = new AccountInfoDto()
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FullName = $"{user.Firstname} {user.Lastname}",
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
            };
            return View(myAccount);
        }
        [Authorize]
        public IActionResult TwoFactorEnabled()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var Result = _userManager.SetTwoFactorEnabledAsync(user, !user.TwoFactorEnabled).Result;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto register)
        {

            if (ModelState.IsValid == false)
            {
                return View(register);
            }
            User newUser = new User()
            {
                Firstname = register.FirstName,
                Lastname = register.LastName,
                Email = register.Email,
                UserName = register.Email,

            };
            var result = _userManager.CreateAsync(newUser, register.Password).Result;
            if (result.Succeeded)
            {
                var token = _userManager.GenerateEmailConfirmationTokenAsync(newUser).Result;
                string callBackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    UserId = newUser.Id,
                    token = token
                }, protocol: Request.Scheme);
                string body = $"Please enter the link sent to your email to confirm the registeration of the account <br/> <a href={callBackUrl}> Link </a>";
                _emailService.Execute(newUser.Email, body, "Confirmation of the User Account");
                return RedirectToAction("DisplayEmail");
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(register);
        }
        public IActionResult ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(UserId).Result;
            if (user == null)
            {
                return View("Error");
            }
            var result = _userManager.ConfirmEmailAsync(user, Token).Result;
            if (result.Succeeded)
            {
                //return
            }
            else
            {

            }
            return RedirectToAction("login");

        }
        public IActionResult DisplayEmail()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {

            return View(new LoginDto
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            if (ModelState.IsValid == false)
            {
                return View(login);
            }
            var user = _userManager.FindByNameAsync(login.UserName).Result;
            _signInManager.SignOutAsync();
            var result = _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true).Result;
            if (result.Succeeded)
            {
                return Redirect(login.ReturnUrl);
            }
            if (result.RequiresTwoFactor == true)
            {
                return RedirectToAction("TwoFactorLogin", new { login.UserName, login.RememberMe });
            }
            if (result.IsLockedOut == true)
            {
                //
            }
            ModelState.AddModelError(string.Empty, "login error");
            return View();
        }
        public IActionResult TwoFactorLogin(string UserName, bool RememberMe)
        {
            var user = _userManager.FindByNameAsync(UserName).Result;

            if (user == null)
            {
                return BadRequest();
            }
            var providers = _userManager.GetValidTwoFactorProvidersAsync(user).Result;
            TwoFactorLoginDto model = new TwoFactorLoginDto();
            if (providers.Contains("Phone"))
            {
                string smsCode = _userManager.GenerateTwoFactorTokenAsync(user, "Phone").Result;
                SmsService sms = new SmsService();
                sms.Send(user.PhoneNumber, smsCode);
                model.Provider = "Phone";
                model.RememberMe = RememberMe;
                 
            }
            else if (providers.Contains("Email"))
            {
                string emailCode = _userManager.GenerateTwoFactorTokenAsync(user, "Email").Result;
                EmailService email = new EmailService();
                email.Execute(user.Email, $"Two-Step vertification Code:{emailCode}", "Two-Step Login");
                model.Provider = "Email";
                model.RememberMe= RememberMe; 
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult TwoFactorLogin(TwoFactorLoginDto twoFactor)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactor);
            }
            var user = _signInManager.GetTwoFactorAuthenticationUserAsync().Result;
            if (user == null)
            {

                return BadRequest();
            }
            var result = _signInManager.TwoFactorSignInAsync(twoFactor.Provider, twoFactor.Code, twoFactor.RememberMe, false).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("index");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account is locked");
                return View();
            }
            return View();
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordConfirmationDto forget)
        {
            if (!ModelState.IsValid)
            {
                return View(forget);
            }
            var user = _userManager.FindByNameAsync(forget.Email).Result;
            if (user == null || _userManager.IsEmailConfirmedAsync(user).Result == false)
            {
                ViewBag.Message = "Entered email is wrong or it's not confirmed!";
                return View();

            }
            string token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            string callBackUrl = Url.Action("ResetPassword", "Account", new
            {
                UserId = user.Id,
                token = token
            }, protocol: Request.Scheme);
            string body = $"Click the link below to reset the password <br/> <a href={callBackUrl} > Link for reset the password </a>";
            _emailService.Execute(user.Email, body, "forgot password");
            ViewBag.Message = "Link has sent to your email to reset the password";
            return View();
        }
        public IActionResult ResetPassword(string UserId, string Token)
        {
            return View(new ResetPasswordDto
            {
                Token = Token,
                UserId = UserId
            });

        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordDto reset)
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }
            if (reset.NewPassword != reset.ConfirmNewPassword)
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(reset.UserId).Result;

            if (user == null)
            {
                return BadRequest();
            }
            var result = _userManager.ResetPasswordAsync(user, reset.Token, reset.NewPassword).Result;
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            else
            {
                ViewBag.Errors = result.Errors;
                return View(reset);
            }


        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        [Authorize]
        public IActionResult SetPhoneNumber()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetPhoneNumber(SetPhoneNumberDto phoneNumberDto)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var setResult = _userManager.SetPhoneNumberAsync(user, phoneNumberDto.PhoneNumber).Result;
            string code = _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumberDto.PhoneNumber).Result;
            SmsService smsService = new SmsService();
            smsService.Send(phoneNumberDto.PhoneNumber, code);
            TempData["PhoneNumber"] = phoneNumberDto.PhoneNumber;
            return RedirectToAction(nameof(VerifyPhoneNumber));

        }
        [Authorize]
        public IActionResult VerifyPhoneNumber()
        {
            return View(new VerifyPhoneNumberDto
            {
                PhoneNumber = TempData["PhoneNumber"].ToString()
            });

        }
        [Authorize]
        [HttpPost]
        public IActionResult VerifyPhoneNumber(VerifyPhoneNumberDto verify)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            bool resultOfVerify = _userManager.VerifyChangePhoneNumberTokenAsync(user, verify.Code, verify.PhoneNumber).Result;
            if (resultOfVerify == false)
            {
                ViewData["Message"] = $"Entered code for number {verify.PhoneNumber} is wrong ";
                return View();
            }
            else
            {
                user.PhoneNumberConfirmed = true;
                _userManager.UpdateAsync(user);
            }
            return RedirectToAction("VerifySuccess");
        }
        public IActionResult VerifySuccess()
        {
            return View();
        }
    }
}
