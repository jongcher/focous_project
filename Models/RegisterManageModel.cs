using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Common;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Common.Class1;

namespace FUCOS.Models.Login
{
    public class RegisterManageModel
    {

        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "ID를 입력하세요")] // Required : 필수입력사항 // AllowEmptyString : 공백허용여부
        [StringLength(25, MinimumLength = 5,
            ErrorMessage = "아이디는 5자 이상 25자 이하로 입력하시오.")] // StringLenght : 문자의 갯수를 제한
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9]).{5,}$", ErrorMessage = "* 아이디는 5~25자의 숫자와 영어의 조합입니다.")] // RegularExpression : 정규식을 사용하여 유효성을 검사
        [Remote("RegisterChkId", "Login")] // Remote : 메서드를 사용하여 유효성을 검사 Remote("매서드","컨트롤러",ErrorMessage="")
        public string? Id { get; set; }    // Register.cshtml 의 input 변수와 일치해야 한다.

        [Required(ErrorMessage = "이름을 입력하세요.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "메일을 입력하세요.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "비밀번호를 입력하세요.")]
        [StringLength(20, ErrorMessage = "비밀번호는 최소8자리 이상 20자리 이하로 구성하세요!", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "* 비밀번호는 8~20자의 영어, 숫자, 특수문자의 조합입니다.")]
        [DataType(DataType.Password)] // 종류선언
        [Display(Name = "Password")] // 이름부여
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password_check", ErrorMessage = "비밀번호와 재입력한 비밀번호가 서로 다릅니다.")]
        // Compare : 서로 비교하여 유효성 검사 // compare("변수명", ErrorMessage="")
        public string? Password_check { get; set; }

        //[Required(ErrorMessage = "생일을 입력하세요.")]
        //[RegularExpression(@"[0-9]{4}-[0-9]{2}-[0-9]{2}$", ErrorMessage = "* 정확한 형식을 입력해 주세요")]
        //public string Mb_Birth { get; set; }

        //[Required(ErrorMessage = "성별을 체크하세요.")]
        //public int? Mb_Gender { get; set; }
        [Required(ErrorMessage = "전화번호를 입력하세요.")]
        [Phone(ErrorMessage = "전화번호가 아닙니다.")]
        public string Tel { get; set; }
        [Required(ErrorMessage = "휴대폰 번호를 입력하세요")]
        [Phone(ErrorMessage = "전화번호가 아닙니다.")] // 휴대전화 형식으로 받기
        public string Phone { get; set; }

        public string? add { get; set; }
        public string? add2 { get; set; }
        public string? add3 { get; set; }

        //기업회원 전용
        //public string? Mb_Ceo_Name { get; set; } //대표 이름

        //public string? Mb_Company_Name { get; set; } //회사명

        //public string? Mb_Biz_Type { get; set; } // 회사분류

        //public string? Mb_Biz_No { get; set; } // 사업자등록번호

        //public string? Mb_Biz_Fax { get; set; }//팩스번호

        //public string? Mb_Biz_Success { get; set; } // 상장여부

        //public string? Mb_Biz_Form { get; set; } // 기업형태

        //public string? Mb_Biz_Foundation { get; set; } //설립연도

        //public string? Mb_Biz_Member_Count { get; set; } //사원수
        //public string? Mb_Biz_Stock { get; set; } //자본금
        //public string? Mb_Biz_Sale { get; set; } //매출액

        //public string? Mb_Biz_Vision { get; set; } //기업 개요 및 비전

        //public string? Mb_Biz_Result { get; set; } // 기업 연혁 및 실적

        //public string? Mb_Class { get; set; }

        //public string? Mb_left_text { get; set; }


        internal DataTable Login_Pass()
        {
            var cmdQry = new MySqlCommand();
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            cmdQry.Parameters.AddWithValue("@P_id", Id);

            var rstQry = connObj.SelQry(conn, cmdQry, "Login_R_000");


            return rstQry;

        }
        internal DataTable C_Login_Pass()
        {
            var cmdQry = new MySqlCommand();
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            cmdQry.Parameters.AddWithValue("@P_id", Id);

            var rstQry = connObj.SelQry(conn, cmdQry, "Login_R_001");


            return rstQry;

        }

        internal string Password_Security()
        {
            Common.Class1.CommonBase connObj = new();

            string S_password = Common.Class1.CommonBase.ConvertSecurity(Id, Password);

            return S_password;
        }

        public DataTable UserSelect()
        {
            var cmdQry = new MySqlCommand();
            Common.Class1.CommonBase connObj = new();             // CommonBasic 클래스 객체 생성
            MySqlConnection conn = connObj.rtnConn();  // 생성객체 내의 함수 사용하여 connection 객체 생성

            cmdQry.Parameters.AddWithValue("@p_id", this.Id);

            var dt = connObj.SelQry(conn, cmdQry, "r_login_000");    //using NeoCommonLib.CommonFunctions;  선언



            // 지겨운 Hello World로 테스트
            var value = "Hello World!";

            // 64 암호화     - 주민번호, 전화번호등을 DB에 저장할때(복호화가 필요할때)
            var enDes = Common.Class1.DES.result(DesType.Encrypt, value);      //using NeoCommonLib.CommonFunctions;    선언
            // 복호화
            var deDes = Common.Class1.DES.result(DesType.Decrypt, enDes);

            // 128, 192, 256 암호화  - 주민번호, 전화번호등을 DB에 저장할때(복호화가 필요할때)
            string enAes = Common.Class1.AES.AESEncrypt(value);
            //복호화
            string deAes = Common.Class1.AES.AESDecrypt(enAes);

            return dt;
        }

        internal int RegisterTrans()   //BeginTransaction을 사용할때
        {
            int rstQry = 0;
            Common.Class1.CommonBase connObj = new();                  // CommonBasic 클래스 객체 생성
            MySqlConnection conn = connObj.rtnConn();  // 생성객체 내의 함수 사용하여 connection 객체 생성

            conn.Open();

            connObj.BeginTrans(conn);     // 트랜젝션 객체 생성
            using (var cmdQry = new MySqlCommand())

            {
                cmdQry.Parameters.AddWithValue("@mb_id", Id);
                cmdQry.Parameters.AddWithValue("@mb_name", Name);
                cmdQry.Parameters.AddWithValue("@mb_password", DES.result(0, Password));
                cmdQry.Parameters.AddWithValue("@mb_phone", Phone);
                cmdQry.Parameters.AddWithValue("@mb_hphone", Tel);
                cmdQry.Parameters.AddWithValue("@mb_zipcode", add);
                cmdQry.Parameters.AddWithValue("@mb_address0", add2);
                cmdQry.Parameters.AddWithValue("@mb_address1", add3);

                try
                {
                    rstQry = connObj.ExeQryTrans(cmdQry, "c_user_000");

                }
                catch
                {
                    connObj.Rollback();      // 쿼리 샐행 실패시 테이블 원상복구
                }
            }
            connObj.Commit();    // 쿼리 성공시 테이블에 실행 내역 적용

            conn.CloseAsync();    //  // 실행 후 Back Graund에서 DB Connnection 닫기

            return rstQry;   // 영향 받은 Row의 갯수
        }
        internal int Register()   //BeginTransaction을 사용하지 않을때 
        {
            int rstQry = 0;
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            string Mb_type = "indivisual";
            string Mb_left = "0";
            if(add == null)
            {
                add = "1";
            }
            if (add2 == null)
            {
                add2 = "1";
            }
            if (add3 == null)
            {
                add3 = "1";
            }

            using (var cmdQry = new MySqlCommand())
            {
                
                cmdQry.Parameters.AddWithValue("@P_ID", Id);
                cmdQry.Parameters.AddWithValue("@P_PW", DES.result(0, Password));
                cmdQry.Parameters.AddWithValue("@P_Name", Name);
                cmdQry.Parameters.AddWithValue("@P_Mail", Mail);
                cmdQry.Parameters.AddWithValue("@P_Tel", Tel);
                cmdQry.Parameters.AddWithValue("@P_Phone", Phone);
                cmdQry.Parameters.AddWithValue("@P_add", add);
                cmdQry.Parameters.AddWithValue("@P_add2", add2);
                cmdQry.Parameters.AddWithValue("@P_add3", add3);

                rstQry = connObj.ExeQry(conn, cmdQry, "Register_C_000");
            }

            return rstQry; // 영향 받은 Row의 갯수
        }

        internal DataTable Chk_Id()
        {
            var cmdQry = new MySqlCommand();
            Common.Class1.CommonBase connObj = new();
            MySqlConnection conn = connObj.rtnConn();

            cmdQry.Parameters.AddWithValue("@mb_id", Id);

            var rstQry = connObj.SelQry(conn, cmdQry, "Login_R_000");


            return rstQry;

        }

        //internal DataTable User_Update()
        //{
        //    var cmdQry = new MySqlCommand();
        //    CommonBasic connObj = new();
        //    MySqlConnection conn = connObj.rtnConn();

        //    if (Mb_zipcode1 == null)
        //    {
        //        Mb_zipcode1 = "1";
        //    }
        //    if (Mb_zipcode2 == null)
        //    {
        //        Mb_zipcode2 = "1";
        //    }
        //    if (Mb_zipcode3 == null)
        //    {
        //        Mb_zipcode3 = "1";
        //    }

        //    cmdQry.Parameters.AddWithValue("@P_id", Mb_Id);
        //    cmdQry.Parameters.AddWithValue("@P_birth", Mb_Birth);
        //    cmdQry.Parameters.AddWithValue("@P_phone", Mb_Phone);
        //    cmdQry.Parameters.AddWithValue("@P_hphone", Mb_Hphone);
        //    cmdQry.Parameters.AddWithValue("@P_zipcode", Mb_zipcode1);
        //    cmdQry.Parameters.AddWithValue("@P_address0", Mb_zipcode2);
        //    cmdQry.Parameters.AddWithValue("@P_address1", Mb_zipcode3);
        //    cmdQry.Parameters.AddWithValue("@P_mail", Mb_Mail);

        //    var rstQry = connObj.SelQry(conn, cmdQry, "Login_U_002");


        //    return rstQry;
        //}
    }
}
