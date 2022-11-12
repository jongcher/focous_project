using FUCOS.Models.Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using static Common.Class1;
using System.Security.Claims;
using System.Web;

namespace FUCOS.Controllers
{
    public class LoginController : Controller
    {
        const string SessionKeyName = "_Name";
        const string SessionKeyFY = "_FY";
        const string SessionKeyDate = "_Date";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/Login/Login")]
        public async Task<IActionResult> LoginCheckAsync([FromForm] RegisterManageModel input)
        {
            var dt = input.Login_Pass();

            int userid_row = dt.Rows.Count;

            try
            {
                if (input.Id != null)
                {
                    if (userid_row == 1)
                    {
                        var user_password = DES.result(0, input.Password); // 비밀번호 암호와

                        string P_password = (string)dt.Rows[0]["USER_PW"];

                        if (P_password != user_password)
                        {
                            throw new Exception("비밀번호를 정확히 입력해주세요.");
                        }

                        string user_id = (string)dt.Rows[0]["USER_ID"];
                        string user_Name = (string)dt.Rows[0]["USER_NM"];
                        string user_Email = (string)dt.Rows[0]["E_MAIL"];
                        string user_Phone = (string)dt.Rows[0]["PHONE"];
                        string user_Hphone = (string)dt.Rows[0]["TEL"];
                        string user_grop = (string)dt.Rows[0]["USER_GROUP"];

                        //세션이용
                        HttpContext.Session.SetString("SessionKeyName", user_Name);
                        HttpContext.Session.SetString("SessionKeyId", user_id);
                        HttpContext.Session.SetString("SessionKeyEmail", user_Email);
                        HttpContext.Session.SetString("SessionKeyPhone", user_Phone);
                        HttpContext.Session.SetString("SessionKeyHphone", user_Hphone);
                        HttpContext.Session.SetString("SessionKeyUserGrop", user_grop);

                        ////쿠키사용
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role); //인증작업 
                        identity.AddClaim(new Claim("Id", user_id.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user_Name.ToString()));

                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties   // 쿠키설정
                        {
                            IsPersistent = false,  //화면을 닫으면 로그인 유지 확인 
                            ExpiresUtc = DateTime.UtcNow.AddHours(4),    // 로그인 후 4시간 유지
                            AllowRefresh = false   // ㅇ
                        });

                        return Redirect("/home/index");

                        throw new Exception("아이디 및 비밀번호를 정확하게 입력해주세요.");

                    }
                    else
                    {
                        throw new Exception("아이디를 정확히 입력해주세요.");
                    }
                }
                else
                {
                    throw new Exception("아이디를 입력하세요.");
                }
            }
            catch (Exception ex)
            {
                return Redirect($"/login/Login?msg={HttpUtility.UrlEncode(ex.Message)}");
            }

        }

        //로그아웃
        [Microsoft.AspNetCore.Authorization.Authorize] //로그인을 한경우에만 사용
        [Route("/Login/Logout")]
        public async Task<IActionResult> Logout()
        {
            //세션이용
            HttpContext.Session.Remove("SessionKeyId");
            HttpContext.Session.Remove("SessionKeyName");
            HttpContext.Session.Remove("SessionKeyEmail");
            HttpContext.Session.Remove("SessionKeyPhone");
            HttpContext.Session.Remove("SessionKeyHphone");
            HttpContext.Session.Remove("SessionKeyUserGrop"); 

            await HttpContext.SignOutAsync();

            return Redirect("/home/index");
        }

    }
}
