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
        public async Task<IActionResult> Login(Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_validator.VerifyLogin(model.Login, out string message))
                {
                    ModelState.AddModelError("", message);
                }
                else if (!_validator.VerifyPassword(model.Password, out message))
                {
                    ModelState.AddModelError("", message);
                }
                else
                {
                    User user = await _userService.GetUser(model.Login, model.Password);
                    user.Role = await _userService.TryGetRole(user.RoleId);

                    if (user != null)
                    {
                        await Authenticate(user); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            else
            {
                int i = ModelState.ErrorCount;
                i++;
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
                if (!_validator.VerifyLogin(model.Login, out string message))
                {
                    ModelState.AddModelError("", message);
                }
                else if (!_validator.VerifyPassword(model.Password, out message))
                {
                    ModelState.AddModelError("", message);
                }
                else if (!_validator.VerifyConfirmPassword(model.Password, model.ConfirmPassword, out message))
                {
                    ModelState.AddModelError("", message);
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
                        ModelState.AddModelError("", _validator.Reporter.LoginBusy);
                    }
                }
            }

            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
            };
            // создаем объект ClaimsIdentity
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
