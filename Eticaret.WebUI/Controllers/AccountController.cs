using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;  //login

namespace Eticaret.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataBaseContext _context;

        public AccountController(DataBaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SingInAsync(LoginViewModel LoginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == LoginViewModel.Email & x.Password == LoginViewModel.Password & x.IsActive);
                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız!");
                    }
                    else
                    {
                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name, account.Name),
                            new(ClaimTypes.Role, account.IsAdmin ? "Admin" : "Customer"),
                            new(ClaimTypes.Email, account.Email),
                            new("UserId", account.Id.ToString()),
                            new("UserGuid", account.UserGuid.ToString()),
                        };
                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincipal);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception hata)
                {
                    //loglama
                    ModelState.AddModelError("","Hata Oluştu!");
                }
            }
            return View(LoginViewModel);
        }
        public IActionResult SingUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SingUpAsync(AppUser appUser)
        {
            appUser.IsAdmin = false;
            appUser.IsActive = true;
            if (ModelState.IsValid)
            {
                await _context.AddAsync(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }
    }
}
