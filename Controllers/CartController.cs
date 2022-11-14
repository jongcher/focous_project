using FUCOS.Models.Login;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using static Common.Class1;

namespace FUCOS.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            string User_ID = HttpContext.Session.GetString("SessionKeyId");

            var cmdQry = new MySqlCommand();
            CommonBase comm = new CommonBase();
            MySqlConnection conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("@P_USER_ID", User_ID);

            var rstQry = comm.SelQry(conn, cmdQry, "CART_R_000");

            List<string> LI_ITEM_CD = new List<string>();
            List<string> LI_ITEM_NM = new List<string>();
            List<string> LI_ITEM_PRICE = new List<string>();
            List<string> LI_UNIT_ITEM = new List<string>();
            List<string> LI_SALE_UNIT = new List<string>();



            if (rstQry.Rows.Count != 0)
            {
                for (int a = 0; a < rstQry.Rows.Count; a++) 
                {
                    string ITEM_CD = rstQry.Rows[a].ItemArray[0].ToString();
                    string ITEM_NM = rstQry.Rows[a].ItemArray[1].ToString();
                    string ITEM_PRICE = Convert.ToInt32(rstQry.Rows[a].ItemArray[2]).ToString();
                    string UNIT_ITEM = rstQry.Rows[a].ItemArray[3].ToString();
                    string SALE_ITEM_CD = rstQry.Rows[a].ItemArray[4].ToString();

                    LI_ITEM_CD.Add(ITEM_CD);
                    LI_ITEM_NM.Add(ITEM_NM);
                    LI_ITEM_PRICE.Add(ITEM_PRICE);
                    LI_UNIT_ITEM.Add(UNIT_ITEM);
                    LI_SALE_UNIT.Add(SALE_ITEM_CD);
                }

            }
            ViewBag.Count = rstQry.Rows.Count;
            ViewBag.LI_ITEM_CD = LI_ITEM_CD;
            ViewBag.LI_ITEM_NM = LI_ITEM_NM;
            ViewBag.LI_ITEM_PRICE = LI_ITEM_PRICE;
            ViewBag.LI_UNIT_ITEM = LI_UNIT_ITEM;
            ViewBag.LI_SALE_UNIT = LI_SALE_UNIT;
            return View();
        }

        public IActionResult Cart_Insert(CartManageModel input)
        {
            string User_ID = HttpContext.Session.GetString("SessionKeyId");
            input.Insert_Cart(User_ID);


            return Redirect("/Mall/MallDetails?ITEM_CD=" + input.ITEM_CD);
        }


        public IActionResult Delete_Cart(CartManageModel input) 
        {
            string User_ID = HttpContext.Session.GetString("SessionKeyId");
            if (User_ID != null)
            {
                if(input.ITEM_CD != "")
                {
                    var cmdQry = new MySqlCommand();
                    CommonBase comm = new CommonBase();
                    MySqlConnection conn = comm.rtnConn();

                    cmdQry.Parameters.AddWithValue("P_USER_ID", User_ID);
                    cmdQry.Parameters.AddWithValue("P_ITEM_CD", input.ITEM_CD);

                    comm.ExeQry(conn, cmdQry, "CART_D_000");

                    
                } 
            }

            return Redirect("/Cart/Cart");
        }
    }
}
