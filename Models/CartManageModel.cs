using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Common;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Common.Class1;
using System.Xml.Linq;
using Microsoft.Extensions.Options;

namespace FUCOS.Models.Login
{
    public class CartManageModel
    {
        public string ITEM_CD { get; set; }

        public bool check { get; set; }


        public void Insert_Cart(string User_ID)
        {
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            using (var cmdQry = new MySqlCommand())
            {
                cmdQry.Parameters.AddWithValue("@P_ITEM_CD", ITEM_CD);
                cmdQry.Parameters.AddWithValue("@P_USER_ID", User_ID);

               connObj.ExeQry(conn, cmdQry, "CART_C_000");
            }

            return;
        }

    }
}
