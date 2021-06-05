using capitulo01.Models.Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace capitulo01.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {
        public UserManager<UsuarioDaAplicacao> UserManager { get; }
        public SignInManager<UsuarioDaAplicacao> SignInManager { get; }
        public ILogger Logger { get; }

        public InfraController(UserManager<UsuarioDaAplicacao> userManager, SignInManager<UsuarioDaAplicacao> signInManager, ILogger<InfraController> logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acessar(AcessarViewModel acessarViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(acessarViewModel.Email, acessarViewModel.Senha, acessarViewModel.LembrarDeMim, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    Logger.LogInformation("Usuário realizou loggin.");
                    return RedirectToLocal(returnUrl);
                }
            }
            ModelState.AddModelError("", "Falha na tentativa de loggin.");
            return View(acessarViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Sair()
        {
            await SignInManager.SignOutAsync();
            Logger.LogInformation("Usuário realizou logout");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrarNovoUsuario(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNovoUsuario([Bind("Email", "Password", "ConfirmPassword")] RegistrarNovoUsuarioViewModel registrarNovoUsuarioViewModel, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new UsuarioDaAplicacao { UserName = registrarNovoUsuarioViewModel.Email, Email = registrarNovoUsuarioViewModel.Email };
                var result = await UserManager.CreateAsync(user, registrarNovoUsuarioViewModel.Password);

                if (result.Succeeded)
                {
                    Logger.LogInformation("Usuário criou um nova conta.");
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    Logger.LogInformation("Usuário acessou.");

                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(registrarNovoUsuarioViewModel);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
