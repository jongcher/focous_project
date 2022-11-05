using FUCOS.Models.Login;
using Microsoft.AspNetCore.Mvc;
using System.Windows;

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

        [HttpPost]
        [Route("/Register/Register")]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterProc([FromForm] RegisterManageModel input)
        {
            if (ModelState.IsValid) 
            {
                input.Register();
                
                return Redirect("/Login/Login");
            }

            return View(input);
        }
    }
}
