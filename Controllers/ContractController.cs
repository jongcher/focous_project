using FUCOS.Models.Login;
using Microsoft.AspNetCore.Mvc;

namespace FUCOS.Controllers
{
    public class ContractController : Controller
    {
        public void Register_Contract(ContractManageModel input)
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
        }

    }
}
