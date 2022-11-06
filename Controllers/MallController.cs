using Microsoft.AspNetCore.Mvc;

namespace FUCOS.Controllers
{
    public class MallController : Controller
    {
        public IActionResult BissniseMall()
        {
            return View();
        }
        public IActionResult franchiseeMall()
        {
            return View();
        }
        public IActionResult NoRegisetMall()
        {
            return View();
        }
        public IActionResult RegisterMall()
        {
            return View();
        }
    }
}
