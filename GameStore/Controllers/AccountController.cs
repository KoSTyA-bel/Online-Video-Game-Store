using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GameStore.Services;
using GameStore.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly AccountValidator _validator;
        private readonly ILogger _logger;

        public AccountController(IUserService userService, AccountValidator validator, ILogger logger)
            : this(userService, validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected AccountController(IUserService userService, AccountValidator validator)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet]
        public virtual IActionResult Login()
        {
            _logger?.LogInformation("Returns a login view result.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Login(Models.LoginModel model, string returnUrl)
        {
            _logger?.LogInformation($"Validate data user login data: login {model.Login}, password {model.Password}");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_validator.VerifyLogin(model.Login) || !_validator.VerifyPassword(model.Password))
            {
                _logger?.LogInformation("Bad data");
                ModelState.AddModelError(string.Empty, "Некорректные логин и(или) пароль");
                return View(model);
            }

            _logger?.LogInformation($"Try fing user with login {model.Login}");
            User user = _userService.GetUser(model.Login);

            if (user == null)
            {
                _logger?.LogInformation($"User with login: {model.Login}, not found");
                ModelState.AddModelError(string.Empty, "Пользователя, с введёнными вами данными, не существует");
                return View(model);
            }

            if (user.Password.Equals(model.Password.GetMD5Hash()))
            {
                ModelState.AddModelError(string.Empty, "Некорректные логин и(или) пароль");
                return View(model);
            }

            _logger?.LogInformation($"User with login {user.Login} succesfuly finded");
            user.Role = _userService.TryGetRole(user.RoleId);
            await Authenticate(user);

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            _logger?.LogInformation("Regirect user to home page");
            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Register()
        {
            _logger?.LogInformation("Returns a register view result.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Register(Models.RegisterModel model)
        {
            _logger?.LogInformation($"Validate data user register data: login {model.Login}, password {model.Password}");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_validator.VerifyData(model.Login, model.Password, model.ConfirmPassword))
            {
                _logger?.LogInformation("Bad data");
                ModelState.AddModelError(string.Empty, "Произошла ошибка сервера.");
                return View(model);
            }

            _logger?.LogInformation($"Try to find user with login {model.Login}");

            if (!_userService.ContainsUser(model.Login))
            {
                _logger?.LogInformation($"Create new user with login {model.Login}");
                _userService.RegistrUser(model.Login, model.Password);
                _logger?.LogInformation($"The user with the login {model.Login} was created successfully. Redirect to login page");

                return RedirectToAction("Login", "Account");
            }

            _logger?.LogInformation($"Login {model.Login} is claimed");
            ModelState.AddModelError(string.Empty, "Login is claimed/");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            this._logger.LogInformation($"User {this.User.Identity.Name} logged out");
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToAction("Index", "Products");
        }

        protected async Task Authenticate(User user)
        {
            this._logger?.LogInformation($"Authenticate user {user.Login}");
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
