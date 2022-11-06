using Microsoft.AspNetCore.Mvc;
using Common;
using static Common.Class1;
using MySqlConnector;

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
            //List<string> Group1_CD = new List<string>();
            //List<string> Group1_NM = new List<string>();
            //List<string> Group2_CD = new List<string>();
            //List<string> Group2_NM = new List<string>();
            //// 접속권한 설정

            ////리스트 몇개 뽑을건지 확인하기
            //var cmdQry = new MySqlCommand();
            //CommonBase comm = new CommonBase();
            //MySqlConnection conn = comm.rtnConn();

            //var rstQry = comm.SelQry(conn, cmdQry, "MALL_R_000");

            //for (int a = 0; a < rstQry.Rows.Count; a++)
            //{
            //    string Group1_item = rstQry.Rows[a].ItemArray[0].ToString();
            //    string Group1_name = rstQry.Rows[a].ItemArray[1].ToString();
            //    Group1_CD.Add(Group1_item);
            //    Group1_NM.Add(Group1_name);
            //}

            //if (rstQry.Rows.Count != 0)
            //{
            //    for (int b = 0; b < Group1_CD.Count; b++)
            //    {
            //        cmdQry = new MySqlCommand();
            //        cmdQry.Parameters.AddWithValue("P_ITEM_LVL_CD", Group1_CD[b].ToString());
            //        rstQry = comm.SelQry(conn, cmdQry, "MALL_R_001");

            //        for(int c = 0; c<rstQry.Rows.Count; c++)
            //        {
            //            string Group2_item_CD = rstQry.Rows[c].ItemArray[0].ToString();
            //            string Group2_item_NM = rstQry.Rows[c].ItemArray[1].ToString();

            //            Group2_CD.Add(Group2_item_CD);
            //            Group2_NM.Add(Group2_item_NM);
            //        }
                    

            //    }
            //}



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
