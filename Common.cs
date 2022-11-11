using MySqlConnector;
using System;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class Class1
    {
        public class AES
        {
            private static readonly string key = "01234567890123456789012345678901"; //32Byte(256Bit) Key
            private static readonly string iv = "3423458901762345"; //16Byte

            //AES 암호화
            public static string AESEncrypt(string input)
            {
                try
                {
                    RijndaelManaged aes = new RijndaelManaged();
                    aes.KeySize = 256; //AES256으로 사용시 
                                       //aes.KeySize = 128; //AES128로 사용시 
                    aes.BlockSize = 128;  //길이 고정
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);    // key 값이 256Bit 라도 aes.KetSize에 따라 결정
                    aes.IV = Encoding.UTF8.GetBytes(iv);
                    var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                    byte[]? buf = null;
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                        {
                            byte[] xXml = Encoding.UTF8.GetBytes(input);
                            cs.Write(xXml, 0, xXml.Length);
                        }
                        buf = ms.ToArray();
                    }
                    string Output = Convert.ToBase64String(buf);
                    return Output;
                }
                catch
                {
                    return "";
                }
            }

            //AES 복호화
            public static string AESDecrypt(string input)
            {
                try
                {
                    RijndaelManaged aes = new RijndaelManaged();
                    aes.KeySize = 256; //AES256으로 사용시 
                                       //aes.KeySize = 128; //AES128로 사용시 
                    aes.BlockSize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);  // key 값이 256Bit 라도 aes.KetSize에 따라 결정
                    aes.IV = Encoding.UTF8.GetBytes(iv);
                    var decrypt = aes.CreateDecryptor();
                    byte[]? buf = null;
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                        {
                            byte[] xXml = Convert.FromBase64String(input);
                            cs.Write(xXml, 0, xXml.Length);
                        }
                        buf = ms.ToArray();
                    }
                    string Output = Encoding.UTF8.GetString(buf);
                    return Output;
                }
                catch
                {
                    return "";
                }
                // AES는 키 길이에 따라 AES-128, AES-192, AES-256으로 나뉘는데 32byte인 현재 글자 수는 AES-256이며
                // 주석 부분만 바꾸면 128이 됩니다. 
                // 참고로 블럭 사이즈는 키 길이와 관계없이 128을 사용해야 합니다.
            }
        }
        public enum DesType
        {
            Encrypt = 0,    // 암호화
            Decrypt = 1     // 복호화
        }
        public class DES   //64비트 암호화
        {

            // Key 값은 무조건 8자리여야한다.
            //        private static byte[] Key { get; set; }


            // 암호화/복호화 메서드
            public static string result(DesType type, string input)
            {
                byte[]? s_Key = Encoding.ASCII.GetBytes("project1");
                byte[]? s_IV = Encoding.ASCII.GetBytes(".netcore");

                var des = new DESCryptoServiceProvider()
                {
                    Key = s_Key,     // Key 값은 무조건 8자리여야한다.
                    IV = s_IV        // 무조건 8자리여야한다.
                };

                var ms = new MemoryStream();

                // 익명 타입으로 transform / data 정의
                var property = new
                {
                    transform = type.Equals(DesType.Encrypt) ? des.CreateEncryptor() : des.CreateDecryptor(),
                    data = type.Equals(DesType.Encrypt) ? Encoding.UTF8.GetBytes(input.ToCharArray()) : Convert.FromBase64String(input)
                };

                var cryStream = new CryptoStream(ms, property.transform, CryptoStreamMode.Write);
                var data = property.data;

                cryStream.Write(data, 0, data.Length);
                cryStream.FlushFinalBlock();

                return type.Equals(DesType.Encrypt) ? Convert.ToBase64String(ms.ToArray()) : Encoding.UTF8.GetString(ms.GetBuffer());
            }
            // 생성자
            //        public DES(string key)
            //        {
            //            Key = ASCIIEncoding.ASCII.GetBytes(key);
            //        }

        }

        public class CommonBase
        {
            MySqlConnection? _conn;
            MySqlTransaction? _trans = null;

            public MySqlConnection rtnConn()
            {
                _conn = new("Server=175.197.124.158;Port=3306;Database=company;Uid=root;Pwd=root;AllowZeroDateTime=True;");

                return _conn;
            }

            public void BeginTrans(MySqlConnection conn)
            {
                _trans = conn.BeginTransaction();
            }

            public void Commit()
            {
                _trans?.Commit();
                _trans = null;
            }
            public void Rollback()
            {
                _trans?.Rollback();
                _trans = null;
            }
            public DataTable SelQry(MySqlConnection conn, MySqlCommand param, string comText)
            {
                var dt = new DataTable();

                param.Connection = conn;
                param.CommandType = CommandType.StoredProcedure;  //저장프로시저를 부르는것으로 설정
                param.CommandText = comText;

                conn.Open();

                dt.Load(param.ExecuteReader());

                conn.Close();

                return dt;
            }
            public int ExeQryTrans(MySqlCommand param, string comText)
            {
                int ExeRst;

                param.Connection = _conn;
                param.Transaction = _trans;    // 적용할 Transaction 객체를 지정
                param.CommandType = CommandType.StoredProcedure;   //저장프로시저를 부르는것으로 설정
                param.CommandText = comText;

                ExeRst = param.ExecuteNonQuery();

                return ExeRst;
            }
            public int ExeQry(MySqlConnection conn, MySqlCommand param, string comText)  // BeginTransaction 이 필요없는 퀴리 사용
            {
                int ExeRst;

                param.Connection = conn;
                param.CommandType = CommandType.StoredProcedure;  //저장프로시저를 부르는것으로 설정
                param.CommandText = comText;

                conn.Open();

                ExeRst = param.ExecuteNonQuery();

                conn.CloseAsync();   // 실행 후 Back Graund에서 DB Connnection 닫기

                return ExeRst;
            }
            public static string ConvertSecurity(string v_id, string v_pw)  //암호화할 기준이 되는 문자열(v_id)과 암호화할 문자열(v_pw)
            {
                var sha = new System.Security.Cryptography.HMACSHA512();
                sha.Key = System.Text.Encoding.UTF8.GetBytes(v_id.Length.ToString());

                var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(v_pw));

                return System.Convert.ToBase64String(hash);
            }
            public string Use_Cookie(ClaimsPrincipal principal, string input)
            {
                string user_name = principal.Identity.Name;

                if (user_name != null)
                {
                    var Value = principal.Claims.FirstOrDefault(c => c.Type.Contains(input)).Value;

                    return Value;
                }
                string user_ull = null;

                return user_ull;
            }
        }
    }
}
