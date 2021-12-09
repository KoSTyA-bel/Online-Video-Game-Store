using GameStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GameStore.Models.Users;

namespace GameStore.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private AccountValidator _validator;

        public AccountController(IUserService userService, AccountValidator validator)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!_validator.VerifyLogin(model.Login))
                {
                }
                else if (!_validator.VerifyPassword(model.Password))
                {
                }
                else
                {
                    User user = await _userService.GetUser(model.Login, model.Password);

                    if (user != null)
                    {
                        user.Role = await _userService.TryGetRole(user.RoleId);
                        await Authenticate(user); // аутентификация

                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            returnUrl = "/";
                        }

                        return Redirect(returnUrl);
                    }

                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Models.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_validator.VerifyData(model.Login, model.Password, model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Произошла ошибка сервера.");
                }
                else
                {
                    if (!await _userService.ContainsUser(model.Login))
                    {
                        await _userService.RegistrUser(model.Login, model.Password);

                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "LOgin claimed/");
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Products");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
