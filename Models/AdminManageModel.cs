using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Common;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Common.Class1;
using System.Xml.Linq;

namespace FUCOS.Models.Login
{
    public class AdminManageModel
    {
        public string ITEM_NM { get; set; }
        public string ITEM_CD { get; set; }
        public string UNIT_ITEM { get; set; }
        public string ITEM_SIZE { get; set; }
        public string ITEM_LVL1 { get; set; }
        public string ITEM_LVL2 { get; set; }
        public string ITEM_LVL3 { get; set; }
        public string STOCK_UNIT { get; set; }
        public string SALE_UNIT { get; set; }
        public string ITEM_PRICE { get; set; }


        public string OPTION_TYPE { get; set; }

        public string SEQ { get; set; }
        public string OPTION_CD { get; set; }
        public string OPTION_NM { get; set; }



        public int Register_ITEM()
        {
            int rstQry = 0;
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            using (var cmdQry = new MySqlCommand())
            {

                cmdQry.Parameters.AddWithValue("@P_ITEM_CD", ITEM_CD);
                cmdQry.Parameters.AddWithValue("@P_ITEM_NM", ITEM_NM);
                cmdQry.Parameters.AddWithValue("@P_UNIT_ITEM", UNIT_ITEM);
                cmdQry.Parameters.AddWithValue("@P_ITEM_SIZE", ITEM_SIZE);
                cmdQry.Parameters.AddWithValue("@P_ITEM_LVL1", ITEM_LVL1);
                cmdQry.Parameters.AddWithValue("@P_ITEM_LVL2", ITEM_LVL2);
                cmdQry.Parameters.AddWithValue("@P_ITEM_LVL3", ITEM_LVL3);
                cmdQry.Parameters.AddWithValue("@P_STOCK_UNIT", STOCK_UNIT);
                cmdQry.Parameters.AddWithValue("@P_SALE_UNIT", SALE_UNIT);
                cmdQry.Parameters.AddWithValue("@P_ITEM_PRICE", ITEM_PRICE);
                cmdQry.Parameters.AddWithValue("@P_ID_INSERT", "admin");


                rstQry = connObj.ExeQry(conn, cmdQry, "REGISTER_ITEM_C_000");
            }

            return rstQry;
        }

        public int Register_Item_Option()
        {
            int rstQry = 0;
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            using (var cmdQry = new MySqlCommand())
            {

                cmdQry.Parameters.AddWithValue("@P_ITEM_CD", ITEM_CD);
                cmdQry.Parameters.AddWithValue("@P_OPTION_TYPE", OPTION_TYPE);
                cmdQry.Parameters.AddWithValue("@P_SEQ", SEQ);
                cmdQry.Parameters.AddWithValue("@P_OPTION_CD", OPTION_CD);
                cmdQry.Parameters.AddWithValue("@P_OPTION_NM", OPTION_NM);


                rstQry = connObj.ExeQry(conn, cmdQry, "REGISTER_ITEM_OPTION_C_000");
            }

            return rstQry;
        }


    }
}
