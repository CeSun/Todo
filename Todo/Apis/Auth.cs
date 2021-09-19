using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Todo.Apis
{
    /*
    public class AuthApi
    {
        public static Timer timer = new Timer();
        public static string Host = "https://login.microsoftonline.com";
        public static async Task<AuthResponse> AuthAsync(string code)
        {
            try
            {
                var postDataStr = "client_id=b600f125-dd3b-4d5b-a331-0bc8007795b6&" +
                                  "scope=offline_access%20Tasks.ReadWrite%20Tasks.ReadWrite.Shared&" +
                                  "code=" + code + "&" +
                                  "redirect_uri=TodoCrossPlatform%3A%2F%2Fauth&" +
                                  "grant_type=authorization_code&" +
                                  "client_secret=";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Host + "/common/oauth2/v2.0/token");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                await myStreamWriter.WriteAsync(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = await myStreamReader.ReadToEndAsync();
                myStreamReader.Close();
                myResponseStream.Close();
                Console.WriteLine(retString);
                var rtl = JsonConvert.DeserializeObject<AuthResponse>(retString);
                _ = Update();
                rtl.GetTime = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                return rtl;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public static async Task Update()
        {
            await ReFresh();
            while (true)
            {
                await Task.Delay((GlobalValue.Instance.AuthInfo.ExpiresIn - 20) * 1000);
               
                ReFresh();

            }
        }

        private static async Task ReFresh()
        {
            try
            {
                var postDataStr = "client_id=b600f125-dd3b-4d5b-a331-0bc8007795b6&" +
                                        "scope=offline_access%20Tasks.ReadWrite%20Tasks.ReadWrite.Shared&" +
                                         "refresh_token=" + GlobalValue.Instance.AuthInfo.RefreshTokn + "&" +
                                         "redirect_uri=TodoCrossPlatform%3A%2F%2Fauth&" +
                                         "grant_type=refresh_token&" +
                                         "client_secret=";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Host + "/common/oauth2/v2.0/token");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                await myStreamWriter.WriteAsync(postDataStr);
                myStreamWriter.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = await myStreamReader.ReadToEndAsync();
                myStreamReader.Close();
                myResponseStream.Close();
                var rtl = JsonConvert.DeserializeObject<AuthResponse>(retString);
                GlobalValue.Instance.AuthInfo = rtl;
                rtl.GetTime = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                Console.WriteLine("refresh ok");
            }
            catch (Exception e)
            {
                Console.WriteLine("refresh failed");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }*/
}
