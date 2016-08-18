using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace testBot.Utils
{
    public class TuringUtils
    {
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();
        public static string GetTuringAnswer(string userid,string msg) {
            string response = postHttp(userid,msg);
            return getRepsonseAnswer(response);
        }
        private static string postHttp(string userid, string msg)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.tuling123.com/openapi/api");

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;
            
            byte[] btBodys = Encoding.UTF8.GetBytes(getRequestJsonString(userid,msg));
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();

            return responseContent;
        }
        private static string getRequestJsonString(string uid, string msg) {
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("key", "30a5bdc51dce4d3799e9476e1a363d1a");
            body.Add("info", msg);
            body.Add("userid", uid);
            return serializer.Serialize(body);
        }
        private static string getRepsonseAnswer(string resposne) {
            Dictionary<string, string> map = null;
            map=(Dictionary<string,string>)serializer.Deserialize(resposne, typeof(Dictionary<string, string>));
            string answer;
            if (map != null && map.TryGetValue("text", out answer))
                return answer;
            return "!#$%@%^@#$%!";
        }

    }

}