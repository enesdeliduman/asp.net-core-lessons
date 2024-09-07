using IdentityApp.Helpers;
using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IdentityApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Account", new { user.Id, token });

                    await _emailSender.SendEmailAsync(user.Email, "Hesap onayi", $"Lutfen hesabinizi onaylamak icin <a href='http://localhost:5222{url}'>linke tiklayiniz</a>");

                    TempDataHelper.SetTempDataMessage(this, "Email hesabinizdaki onay linkine tiklayiniz", "warning");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string Token)
        {
            if (Id == null | Token == null)
            {
                TempDataHelper.SetTempDataMessage(this, "Gecersiz token bilgisi", "warning");
                return View();
            }
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, Token);
                if (result.Succeeded)
                {
                    TempDataHelper.SetTempDataMessage(this, "Hesabiniz onaylandi", "success");
                    return View();
                }
            }
            TempDataHelper.SetTempDataMessage(this, "Kullanici bulunamadi", "error");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "E-posta adresiniz doğrulanmamış. Lütfen e-posta adresinizi doğrulayın.");
                        return View(model);
                    }
                    if (result.Succeeded)
                    {

                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError("", $"Cok fazla hatali giris yaptin. Lutfen {timeLeft.Minutes + 1} dakika sonra tekrar dene");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Parolaniz hatali");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Boyle bir kullanici bulunamadi");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempDataHelper.SetTempDataMessage(this, "Lutfen mail adresinizi giriniz", "warning");
                return RedirectToAction("ForgotPassword");
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                TempDataHelper.SetTempDataMessage(this, "Bu emaille baglantili bir hesap bulamadik", "warning");
                return RedirectToAction("ForgotPassword");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new { user.Id, token });
            await _emailSender.SendEmailAsync(Email, "Parola Sifirlama", $"Parolanizi sifirlamak icin <a href='http://localhost:5222{url}'>linke tiklayiniz</a>");

            TempDataHelper.SetTempDataMessage(this, "Parola sifirlama baglantisi basariyla gonderildi. Lutfen posta kutunuzu kontorl ediniz", "success");
            return View();
        }
        public IActionResult ResetPassword(string Id, string Token)
        {
            if (Id == null || Token == null)
            {
                return NotFound();
            }
            var model = new ResetPasswordViewModel { Token = Token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempDataHelper.SetTempDataMessage(this, "Parolaniz basariyla degistirildi", "success");
                return RedirectToAction("Login");
            }
            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
                return View();
            }

            return RedirectToAction("Index");

        }
    }
}
