using Microsoft.AspNetCore.Mvc;

namespace FUCOS.Controllers
{
    public class RegisterController : Controller
    {



        [HttpGet]
        [Route("/Register/Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
