using FUCOS.Models.Login;
using Microsoft.AspNetCore.Mvc;

namespace FUCOS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult RegisterItem()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }
        
        public IActionResult RegisterItemOption()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterItem(AdminManageModel input)
        {
            if(input.ITEM_CD == "" || input.ITEM_CD ==null)
            {
                return View(input);
            }
            if (input.ITEM_NM == "" || input.ITEM_NM == null)
            {
                return View(input);
            }

            input.Register_ITEM();

            return Redirect("/Admin/RegisterItem");
        }

        [HttpGet]
        public IActionResult RegisterItemOption(AdminManageModel input)
        {
            if (input.ITEM_CD == "" || input.ITEM_CD == null)
            {
                return View(input);
            }
            if (input.OPTION_CD == "" || input.OPTION_CD == null)
            {
                return View(input);
            }

            input.Register_Item_Option();
            return Redirect("/Admin/RegisterItemOption");
        }

    }
}
