using FUCOS.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Windows;
using static Common.Class1;

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

        //아이디 유효성 검사
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("Register/RegisterChkId")]
        public ActionResult RegisterChkId(RegisterManageModel input)
        {
            var dt = new DataTable();

            dt = input.Chk_Id();

            ViewData["dt"] = dt;

            int userid_row = dt.Rows.Count;

            if (userid_row == 1)
            {
                return Json($"아이디가 중복 되었습니다.");
            }
            else
            {
                return Json(true);
            }

        }
        
    }
}
