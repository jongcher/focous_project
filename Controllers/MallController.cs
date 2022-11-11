using Microsoft.AspNetCore.Mvc;
using Common;
using static Common.Class1;
using MySqlConnector;
using FUCOS.Models.Login;

namespace FUCOS.Controllers
{
    public class MallController : Controller
    {
        public IActionResult BissniseMall(MallManageModel input)
        {
            string user_group = HttpContext.Session.GetString("SessionKeyUserGrop");

            if (user_group == "null" || user_group != "Customer_USER")
            {
                ModelState.AddModelError("경고", "사업자 및 가맹점만 열람 가능합니다.");
                return Redirect("/");
            }

            string ITEM_NM = input.ITEM_NM;
            ////리스트 몇개 뽑을건지 확인하기
            if (input.ITEM_NM == "All_ITEM" || input.ITEM_NM == null)
            {
                input.ITEM_NM = "";
            }

            var cmdQry = new MySqlCommand();
            CommonBase comm = new CommonBase();
            MySqlConnection conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("P_ITEM_NM", input.ITEM_NM);

            var rstQry = comm.SelQry(conn, cmdQry, "ITEM_R_000");

            int item_count = rstQry.Rows.Count;
            int container_count = 0;
            int num = 0;

            if (item_count % 2 == 1)
            {
                container_count = item_count / 2 + 1;
            }
            else
            {
                container_count = item_count / 2;
            }

            List<string> list_item_cd = new List<string>();
            List<string> list_ITEM_NM = new List<string>();
            List<string> list_UNIT_ITEM = new List<string>();
            List<string> list_ITEM_SIZE = new List<string>();
            List<string> list_item_lvl1 = new List<string>();
            List<string> list_item_lvl2 = new List<string>();
            List<string> list_item_lvl3 = new List<string>();
            List<string> list_SALE_UNIT = new List<string>();
            List<string> list_ITEM_PRICE = new List<string>();

            for (int a = 0; a < rstQry.Rows.Count; a++)
            {
                if (rstQry.Rows[a].ItemArray[0] != null)
                {
                    list_item_cd.Add(rstQry.Rows[a].ItemArray[0].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[1] != null)
                {
                    list_ITEM_NM.Add(rstQry.Rows[a].ItemArray[1].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[2] != null)
                {
                    list_UNIT_ITEM.Add(rstQry.Rows[a].ItemArray[2].ToString());
                }
                else
                {
                    list_UNIT_ITEM.Add("");
                }

                if (rstQry.Rows[a].ItemArray[3] != null)
                {
                    list_ITEM_SIZE.Add(rstQry.Rows[a].ItemArray[3].ToString());
                }
                else
                {
                    list_ITEM_SIZE.Add("");
                }

                if (rstQry.Rows[a].ItemArray[4] != null)
                {
                    list_item_lvl1.Add(rstQry.Rows[a].ItemArray[4].ToString());
                }
                else
                {
                    list_item_lvl1.Add("");
                }

                if (rstQry.Rows[a].ItemArray[5] != null)
                {
                    list_item_lvl2.Add(rstQry.Rows[a].ItemArray[5].ToString());
                }
                else
                {
                    list_item_lvl2.Add("");
                }

                if (rstQry.Rows[a].ItemArray[6] != null)
                {
                    list_item_lvl3.Add(rstQry.Rows[a].ItemArray[6].ToString());
                }
                else
                {
                    list_item_lvl3.Add("");
                }

                if (rstQry.Rows[a].ItemArray[7] != null)
                {
                    list_SALE_UNIT.Add(rstQry.Rows[a].ItemArray[7].ToString());
                }
                else
                {
                    list_SALE_UNIT.Add("");
                }

                if (rstQry.Rows[a].ItemArray[8] != null)
                {
                    list_ITEM_PRICE.Add(Convert.ToInt32(rstQry.Rows[a].ItemArray[8]).ToString());
                }
                else
                {
                    list_ITEM_PRICE.Add("");
                }

            }

            ViewBag.list_item_cd = list_item_cd;
            ViewBag.list_ITEM_NM = list_ITEM_NM;
            ViewBag.list_UNIT_ITEM = list_UNIT_ITEM;
            ViewBag.list_ITEM_SIZE = list_ITEM_SIZE;
            ViewBag.list_item_lvl1 = list_item_lvl1;
            ViewBag.list_item_lvl2 = list_item_lvl2;
            ViewBag.list_item_lvl3 = list_item_lvl3;
            ViewBag.list_SALE_UNIT = list_SALE_UNIT;
            ViewBag.list_ITEM_PRICE = list_ITEM_PRICE;

            ViewBag.item_count = item_count;
            ViewBag.container_count = container_count;
            return View();
        }

        //[Route("Mall/franchiseeMall/?{ITEM_NM}")]
        public IActionResult franchiseeMall(MallManageModel input)
        {
            //// 접속권한 설정

            string user_group = HttpContext.Session.GetString("SessionKeyUserGrop");

            if (user_group == null)
            {
                ModelState.AddModelError("경고", "사업자 및 가맹점만 열람 가능합니다.");
                return Redirect("/");
            }

            if (user_group == "Customer_USER")
            {
                ModelState.AddModelError("경고", "사업자 및 가맹점만 열람 가능합니다.");
                return Redirect("/");
            }

            string ITEM_NM = input.ITEM_NM;
            ////리스트 몇개 뽑을건지 확인하기
            if (input.ITEM_NM == "All_ITEM" || input.ITEM_NM == null)
            {
                input.ITEM_NM = "";
            }

            var cmdQry = new MySqlCommand();
            CommonBase comm = new CommonBase();
            MySqlConnection conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("P_ITEM_NM", input.ITEM_NM);

            var rstQry = comm.SelQry(conn, cmdQry, "ITEM_R_000");

            int item_count = rstQry.Rows.Count;
            int container_count = 0;
            int num = 0;

            if (item_count % 2 == 1)
            {
                container_count = item_count / 2 + 1;
            }
            else
            {
                container_count = item_count / 2;
            }

            List<string> list_item_cd = new List<string>();
            List<string> list_ITEM_NM = new List<string>();
            List<string> list_UNIT_ITEM = new List<string>();
            List<string> list_ITEM_SIZE = new List<string>();
            List<string> list_item_lvl1 = new List<string>();
            List<string> list_item_lvl2 = new List<string>();
            List<string> list_item_lvl3 = new List<string>();
            List<string> list_SALE_UNIT = new List<string>();
            List<string> list_ITEM_PRICE = new List<string>();

            for (int a = 0; a < rstQry.Rows.Count; a++)
            {
                if (rstQry.Rows[a].ItemArray[0] != null)
                {
                    list_item_cd.Add(rstQry.Rows[a].ItemArray[0].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[1] != null)
                {
                    list_ITEM_NM.Add(rstQry.Rows[a].ItemArray[1].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[2] != null)
                {
                    list_UNIT_ITEM.Add(rstQry.Rows[a].ItemArray[2].ToString());
                }
                else
                {
                    list_UNIT_ITEM.Add("");
                }

                if (rstQry.Rows[a].ItemArray[3] != null)
                {
                    list_ITEM_SIZE.Add(rstQry.Rows[a].ItemArray[3].ToString());
                }
                else
                {
                    list_ITEM_SIZE.Add("");
                }

                if (rstQry.Rows[a].ItemArray[4] != null)
                {
                    list_item_lvl1.Add(rstQry.Rows[a].ItemArray[4].ToString());
                }
                else
                {
                    list_item_lvl1.Add("");
                }

                if (rstQry.Rows[a].ItemArray[5] != null)
                {
                    list_item_lvl2.Add(rstQry.Rows[a].ItemArray[5].ToString());
                }
                else
                {
                    list_item_lvl2.Add("");
                }

                if (rstQry.Rows[a].ItemArray[6] != null)
                {
                    list_item_lvl3.Add(rstQry.Rows[a].ItemArray[6].ToString());
                }
                else
                {
                    list_item_lvl3.Add("");
                }

                if (rstQry.Rows[a].ItemArray[7] != null)
                {
                    list_SALE_UNIT.Add(rstQry.Rows[a].ItemArray[7].ToString());
                }
                else
                {
                    list_SALE_UNIT.Add("");
                }

                if (rstQry.Rows[a].ItemArray[8] != null)
                {
                    list_ITEM_PRICE.Add(Convert.ToInt32(rstQry.Rows[a].ItemArray[8]).ToString());
                }
                else
                {
                    list_ITEM_PRICE.Add("");
                }

            }

            ViewBag.list_item_cd = list_item_cd;
            ViewBag.list_ITEM_NM = list_ITEM_NM;
            ViewBag.list_UNIT_ITEM = list_UNIT_ITEM;
            ViewBag.list_ITEM_SIZE = list_ITEM_SIZE;
            ViewBag.list_item_lvl1 = list_item_lvl1;
            ViewBag.list_item_lvl2 = list_item_lvl2;
            ViewBag.list_item_lvl3 = list_item_lvl3;
            ViewBag.list_SALE_UNIT = list_SALE_UNIT;
            ViewBag.list_ITEM_PRICE = list_ITEM_PRICE;

            ViewBag.item_count = item_count;
            ViewBag.container_count = container_count;

            return View();
        }
        public IActionResult NoRegisetMall(MallManageModel input)
        {
            string ITEM_NM = input.ITEM_NM;
            ////리스트 몇개 뽑을건지 확인하기
            if (input.ITEM_NM == "All_ITEM" || input.ITEM_NM == null)
            {
                input.ITEM_NM = "";
            }

            var cmdQry = new MySqlCommand();
            CommonBase comm = new CommonBase();
            MySqlConnection conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("P_ITEM_NM", input.ITEM_NM);

            var rstQry = comm.SelQry(conn, cmdQry, "ITEM_R_000");

            int item_count = rstQry.Rows.Count;
            int container_count = 0;
            int num = 0;

            if (item_count % 2 == 1)
            {
                container_count = item_count / 2 + 1;
            }
            else
            {
                container_count = item_count / 2;
            }

            List<string> list_item_cd = new List<string>();
            List<string> list_ITEM_NM = new List<string>();
            List<string> list_UNIT_ITEM = new List<string>();
            List<string> list_ITEM_SIZE = new List<string>();
            List<string> list_item_lvl1 = new List<string>();
            List<string> list_item_lvl2 = new List<string>();
            List<string> list_item_lvl3 = new List<string>();
            List<string> list_SALE_UNIT = new List<string>();
            List<string> list_ITEM_PRICE = new List<string>();

            for (int a = 0; a < rstQry.Rows.Count; a++)
            {
                if (rstQry.Rows[a].ItemArray[0] != null)
                {
                    list_item_cd.Add(rstQry.Rows[a].ItemArray[0].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[1] != null)
                {
                    list_ITEM_NM.Add(rstQry.Rows[a].ItemArray[1].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[2] != null)
                {
                    list_UNIT_ITEM.Add(rstQry.Rows[a].ItemArray[2].ToString());
                }
                else
                {
                    list_UNIT_ITEM.Add("");
                }

                if (rstQry.Rows[a].ItemArray[3] != null)
                {
                    list_ITEM_SIZE.Add(rstQry.Rows[a].ItemArray[3].ToString());
                }
                else
                {
                    list_ITEM_SIZE.Add("");
                }

                if (rstQry.Rows[a].ItemArray[4] != null)
                {
                    list_item_lvl1.Add(rstQry.Rows[a].ItemArray[4].ToString());
                }
                else
                {
                    list_item_lvl1.Add("");
                }

                if (rstQry.Rows[a].ItemArray[5] != null)
                {
                    list_item_lvl2.Add(rstQry.Rows[a].ItemArray[5].ToString());
                }
                else
                {
                    list_item_lvl2.Add("");
                }

                if (rstQry.Rows[a].ItemArray[6] != null)
                {
                    list_item_lvl3.Add(rstQry.Rows[a].ItemArray[6].ToString());
                }
                else
                {
                    list_item_lvl3.Add("");
                }

                if (rstQry.Rows[a].ItemArray[7] != null)
                {
                    list_SALE_UNIT.Add(rstQry.Rows[a].ItemArray[7].ToString());
                }
                else
                {
                    list_SALE_UNIT.Add("");
                }

                if (rstQry.Rows[a].ItemArray[8] != null)
                {
                    list_ITEM_PRICE.Add(Convert.ToInt32(rstQry.Rows[a].ItemArray[8]).ToString());
                }
                else
                {
                    list_ITEM_PRICE.Add("");
                }

            }

            ViewBag.list_item_cd = list_item_cd;
            ViewBag.list_ITEM_NM = list_ITEM_NM;
            ViewBag.list_UNIT_ITEM = list_UNIT_ITEM;
            ViewBag.list_ITEM_SIZE = list_ITEM_SIZE;
            ViewBag.list_item_lvl1 = list_item_lvl1;
            ViewBag.list_item_lvl2 = list_item_lvl2;
            ViewBag.list_item_lvl3 = list_item_lvl3;
            ViewBag.list_SALE_UNIT = list_SALE_UNIT;
            ViewBag.list_ITEM_PRICE = list_ITEM_PRICE;

            ViewBag.item_count = item_count;
            ViewBag.container_count = container_count;
            return View();
        }
        public IActionResult RegisterMall(MallManageModel input)
        {
            string ITEM_NM = input.ITEM_NM;
            ////리스트 몇개 뽑을건지 확인하기
            if (input.ITEM_NM == "All_ITEM" || input.ITEM_NM == null)
            {
                input.ITEM_NM = "";
            }

            var cmdQry = new MySqlCommand();
            CommonBase comm = new CommonBase();
            MySqlConnection conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("P_ITEM_NM", input.ITEM_NM);

            var rstQry = comm.SelQry(conn, cmdQry, "ITEM_R_000");

            int item_count = rstQry.Rows.Count;
            int container_count = 0;
            int num = 0;

            if (item_count % 2 == 1)
            {
                container_count = item_count / 2 + 1;
            }
            else
            {
                container_count = item_count / 2;
            }

            List<string> list_item_cd = new List<string>();
            List<string> list_ITEM_NM = new List<string>();
            List<string> list_UNIT_ITEM = new List<string>();
            List<string> list_ITEM_SIZE = new List<string>();
            List<string> list_item_lvl1 = new List<string>();
            List<string> list_item_lvl2 = new List<string>();
            List<string> list_item_lvl3 = new List<string>();
            List<string> list_SALE_UNIT = new List<string>();
            List<string> list_ITEM_PRICE = new List<string>();

            for (int a = 0; a < rstQry.Rows.Count; a++)
            {
                if (rstQry.Rows[a].ItemArray[0] != null)
                {
                    list_item_cd.Add(rstQry.Rows[a].ItemArray[0].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[1] != null)
                {
                    list_ITEM_NM.Add(rstQry.Rows[a].ItemArray[1].ToString());
                }
                else
                {
                    list_item_cd.Add("");
                }

                if (rstQry.Rows[a].ItemArray[2] != null)
                {
                    list_UNIT_ITEM.Add(rstQry.Rows[a].ItemArray[2].ToString());
                }
                else
                {
                    list_UNIT_ITEM.Add("");
                }

                if (rstQry.Rows[a].ItemArray[3] != null)
                {
                    list_ITEM_SIZE.Add(rstQry.Rows[a].ItemArray[3].ToString());
                }
                else
                {
                    list_ITEM_SIZE.Add("");
                }

                if (rstQry.Rows[a].ItemArray[4] != null)
                {
                    list_item_lvl1.Add(rstQry.Rows[a].ItemArray[4].ToString());
                }
                else
                {
                    list_item_lvl1.Add("");
                }

                if (rstQry.Rows[a].ItemArray[5] != null)
                {
                    list_item_lvl2.Add(rstQry.Rows[a].ItemArray[5].ToString());
                }
                else
                {
                    list_item_lvl2.Add("");
                }

                if (rstQry.Rows[a].ItemArray[6] != null)
                {
                    list_item_lvl3.Add(rstQry.Rows[a].ItemArray[6].ToString());
                }
                else
                {
                    list_item_lvl3.Add("");
                }

                if (rstQry.Rows[a].ItemArray[7] != null)
                {
                    list_SALE_UNIT.Add(rstQry.Rows[a].ItemArray[7].ToString());
                }
                else
                {
                    list_SALE_UNIT.Add("");
                }

                if (rstQry.Rows[a].ItemArray[8] != null)
                {
                    list_ITEM_PRICE.Add(Convert.ToInt32(rstQry.Rows[a].ItemArray[8]).ToString());
                }
                else
                {
                    list_ITEM_PRICE.Add("");
                }

            }

            ViewBag.list_item_cd = list_item_cd;
            ViewBag.list_ITEM_NM = list_ITEM_NM;
            ViewBag.list_UNIT_ITEM = list_UNIT_ITEM;
            ViewBag.list_ITEM_SIZE = list_ITEM_SIZE;
            ViewBag.list_item_lvl1 = list_item_lvl1;
            ViewBag.list_item_lvl2 = list_item_lvl2;
            ViewBag.list_item_lvl3 = list_item_lvl3;
            ViewBag.list_SALE_UNIT = list_SALE_UNIT;
            ViewBag.list_ITEM_PRICE = list_ITEM_PRICE;

            ViewBag.item_count = item_count;
            ViewBag.container_count = container_count;
            return View();
        }

        public IActionResult MallDetails(MallManageModel input)
        {
            string user_group = HttpContext.Session.GetString("SessionKeyUserGrop");
            ////리스트 몇개 뽑을건지 확인하기
            if (input.ITEM_CD == "All_ITEM" || input.ITEM_CD == null)
            {
                input.ITEM_CD = "";
            }

            var cmdQry = new MySqlCommand();
            CommonBase comm = new CommonBase();
            MySqlConnection conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("P_ITEM_CD", input.ITEM_CD);

            var rstQry = comm.SelQry(conn, cmdQry, "ITEM_R_001");


            cmdQry = new MySqlCommand();
            comm = new CommonBase();
            conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("P_ITEM_CD", input.ITEM_CD);

            var rstQry2 = comm.SelQry(conn, cmdQry, "ITEM_OPTION_R_000");

            if (rstQry.Rows.Count == 0)
            {
                return Redirect("/Mall/franchiseeMall");
            }

            List <string> op_li_NM = new List<string>();
            List<string> op_li_CD = new List<string>();
            if (rstQry2.Rows.Count != 0)
            {
                for (int a = 0; a < rstQry2.Rows.Count; a++)
                {
                    string op_NM;
                    string op_CD;
                    if (rstQry2.Rows[a].ItemArray[1] == null)
                    {
                        op_NM = "";
                    }
                    else
                    {
                        op_NM = rstQry2.Rows[a].ItemArray[1].ToString();
                    }

                    if (rstQry2.Rows[a].ItemArray[0] == null)
                    {
                        op_CD = "";
                    }
                    else
                    {
                        op_CD = rstQry2.Rows[a].ItemArray[0].ToString();
                    }
                    op_li_NM.Add(op_NM);
                    op_li_CD.Add(op_CD);
                }
            }

            ViewBag.ITEM_NM = rstQry.Rows[0].ItemArray[0];
            ViewBag.UNIT_ITEM = rstQry.Rows[0].ItemArray[1];
            ViewBag.ITEM_SIZE = rstQry.Rows[0].ItemArray[2];
            ViewBag.ITEM_PRICE = Convert.ToInt32(rstQry.Rows[0].ItemArray[3]);
            ViewBag.op_li_NM = op_li_NM;
            ViewBag.op_li_CD = op_li_CD;

            return View();
        }

    }
}
