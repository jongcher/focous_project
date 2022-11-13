using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Common;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Common.Class1;

namespace FUCOS.Models.Login
{
    public class MallManageModel
    {
        public string ITEM_NM { get; set; }

        public string ITEM_CD { get; set; }

        public string ITEM_LVL1 { get; set; }

        public string ITEM_LVL2 { get; set; }

        public string ITEM_LVL3 { get; set; }
    }
}
