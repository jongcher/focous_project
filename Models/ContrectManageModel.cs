using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Common;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Common.Class1;
using System.Xml.Linq;

namespace FUCOS.Models.Login
{
    public class ContractManageModel
    {
        public string ITEM_CD { get; set; }
        public string ITEM_SEQ { get; set; }
        public string ITEM_PRICE { get; set; }
        public string BUY_USER_ID { get; set; }
        public string OPTION_M { get; set; }
        public string OPTION_D { get; set; }


        public string CHECK_CO_CD()
        {
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            string CO_CD_LAST = "";
            using (var cmdQry = new MySqlCommand())
            {
                var dt = connObj.SelQry(conn, cmdQry, "CONTRACT_R_000");

                if (dt.Rows.Count != 0) 
                {
                    CO_CD_LAST = dt.Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    CO_CD_LAST = "";
                }

                return CO_CD_LAST;
            }
        }

        public int Register_Contract(string CO_CD)
        {
            int rstQry = 0;
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            using (var cmdQry = new MySqlCommand())
            {

                int PRICE = Convert.ToInt32(ITEM_PRICE) * Convert.ToInt32(ITEM_SEQ); 
                DateTime DT = DateTime.Now;

                string DATE = DT.ToString("yyyyMMdd");


                cmdQry.Parameters.AddWithValue("@P_CO_CD", CO_CD);
                cmdQry.Parameters.AddWithValue("@P_ITEM_CD", ITEM_CD);
                cmdQry.Parameters.AddWithValue("@P_ITEM_SEQ", ITEM_SEQ);
                cmdQry.Parameters.AddWithValue("@P_OPTION_M", OPTION_M);
                cmdQry.Parameters.AddWithValue("@P_OPTION_D", OPTION_D);
                cmdQry.Parameters.AddWithValue("@P_AMT_PRICE", PRICE);
                cmdQry.Parameters.AddWithValue("@P_BUY_USER_ID", BUY_USER_ID);
                cmdQry.Parameters.AddWithValue("@P_CO_DATE", DATE);
                cmdQry.Parameters.AddWithValue("@P_ID_INSERT", BUY_USER_ID);
                cmdQry.Parameters.AddWithValue("@P_ID_UPDATE", BUY_USER_ID);

                rstQry = connObj.ExeQry(conn, cmdQry, "CONTRACT_C_000");
            }

            return rstQry;
        }
    }
}
