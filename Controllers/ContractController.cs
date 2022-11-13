using FUCOS.Models.Login;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using static Common.Class1;

namespace FUCOS.Controllers
{
    public class ContractController : Controller
    {
        public IActionResult Contract() // 사용자 주문현황 페이지
        {
            string USER_ID = HttpContext.Session.GetString("SessionKeyId");

            if(USER_ID == "" || USER_ID == null)
            {
                return Redirect("/");
            }

            var cmdQry = new MySqlCommand();
            CommonBase comm = new CommonBase();
            MySqlConnection conn = comm.rtnConn();

            cmdQry.Parameters.AddWithValue("@P_USER_ID", USER_ID);

            var rstQry = comm.SelQry(conn, cmdQry, "CONTRACT_R_001");

            List<string> LI_CO_CD = new List<string>();
            List<string> LI_ITEM_CD = new List<string>();
            List<string> LI_ITEM_NM = new List<string>();
            List<string> LI_ITEM_SEQ = new List<string>();
            List<string> LI_OPTION_M = new List<string>();
            List<string> LI_OPTION_D = new List<string>();
            List<string> LI_AMT_PRICE = new List<string>();
            List<string> LI_ITEM_PRICE = new List<string>();
            List<string> LI_CO_DATE = new List<string>();

            if (rstQry.Rows.Count != 0)
            {
                for(int a =0; a < rstQry.Rows.Count; a++)
                {
                    string CO_CD = rstQry.Rows[a].ItemArray[0].ToString();
                    string ITEM_CD = rstQry.Rows[a].ItemArray[1].ToString();
                    string ITEM_NM = rstQry.Rows[a].ItemArray[2].ToString();
                    string ITEM_SEQ = rstQry.Rows[a].ItemArray[3].ToString();
                    string OPTION_M = "";
                    if (rstQry.Rows[a].ItemArray[4] == null)
                    {
                        OPTION_M = "옵션 없음";
                    }
                    else
                    {
                        OPTION_M = rstQry.Rows[a].ItemArray[4].ToString();
                    }

                    string OPTION_D = "";
                    if (rstQry.Rows[a].ItemArray[5] == null)
                    {
                        OPTION_D = "옵션 없음";
                    }
                    else
                    {
                        OPTION_D = rstQry.Rows[a].ItemArray[5].ToString();
                    }

                    string AMT_PRICE = Convert.ToInt32(rstQry.Rows[a].ItemArray[6]).ToString();
                    string TEM_PRICE = Convert.ToInt32(rstQry.Rows[a].ItemArray[7]).ToString();
                    string CO_DATE = rstQry.Rows[a].ItemArray[8].ToString();

                    LI_CO_CD.Add(CO_CD);
                    LI_ITEM_CD.Add(ITEM_CD);
                    LI_ITEM_NM.Add(ITEM_NM);
                    LI_ITEM_SEQ.Add(ITEM_SEQ);
                    LI_OPTION_M.Add(OPTION_M);
                    LI_OPTION_D.Add(OPTION_D);
                    LI_AMT_PRICE.Add(AMT_PRICE);
                    LI_ITEM_PRICE.Add(TEM_PRICE);
                    LI_CO_DATE.Add(CO_DATE);
                }
            }

            ViewBag.COUNT = rstQry.Rows.Count;
            ViewBag.LI_CO_CD = LI_CO_CD;
            ViewBag.LI_ITEM_CD = LI_ITEM_CD;
            ViewBag.LI_ITEM_NM = LI_ITEM_NM;
            ViewBag.LI_ITEM_SEQ = LI_ITEM_SEQ;
            ViewBag.LI_OPTION_M = LI_OPTION_M;
            ViewBag.LI_OPTION_D = LI_OPTION_D;
            ViewBag.LI_AMT_PRICE = LI_AMT_PRICE;
            ViewBag.LI_ITEM_PRICE = LI_ITEM_PRICE;
            ViewBag.LI_CO_DATE = LI_CO_DATE;


            return View();
        }
        public IActionResult Register_Contract(ContractManageModel input)
        {
            string CO_CD = "";
            string CO_CD_LAST = input.CHECK_CO_CD();
            string DATE = DateTime.Now.ToString("yyyyMMdd");

            if (CO_CD_LAST == "") 
            {
                CO_CD = "CO" + DATE + "00001";
            }
            else
            {
                string LAST_YEAR = CO_CD_LAST.Substring(2, 4);
                string LAST_MONTH = CO_CD_LAST.Substring(6, 2);
                string LAST_DAY = CO_CD_LAST.Substring(8, 2);
                string LAST_NO = CO_CD_LAST.Substring(10, 5);

                if(LAST_YEAR == DateTime.Now.ToString("yyyy"))
                {
                    if(LAST_MONTH == DateTime.Now.ToString("MM"))
                    {
                        if(LAST_DAY == DateTime.Now.ToString("dd"))
                        {
                            string NO = (Convert.ToInt32(LAST_DAY) +1 ).ToString();
                            NO.PadLeft(5, '0');

                            CO_CD = "CO" + DATE + NO;

                        }
                    }
                }
                CO_CD = "CO" + DATE + "00001";
            }

            input.Register_Contract(CO_CD);
            return Redirect("/Mall/BuyComplyte");
        }

    }
}
