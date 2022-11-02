using Microsoft.AspNetCore.Mvc;

namespace FUCOS.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
