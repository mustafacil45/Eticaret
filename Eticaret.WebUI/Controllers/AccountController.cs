using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SingIn()
        {
            return View();
        }
        public IActionResult SingUp()
        {
            return View();
        }
    }
}
