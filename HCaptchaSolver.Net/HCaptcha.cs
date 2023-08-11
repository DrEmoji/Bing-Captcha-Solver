using HCaptchaSolver.Net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HCaptchaSolver.Net
{
    internal class HCaptcha
    {
        public static async Task<string> Solve(string website, string widgetid, string host, string sitekey)
        {
            API.Client = new HttpClient(new HttpClientHandler() { UseCookies = true, CookieContainer = new CookieContainer() });
            API.Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/115.0.0.0 Safari/537.36");
            API.Client.DefaultRequestHeaders.Add("Origin", "https://newassets.hcaptcha.com");
            API.Client.DefaultRequestHeaders.Add("Referer", "https://newassets.hcaptcha.com/");
            WebProxy Proxy = new WebProxy
            {
                Address = new Uri("http://us.smartproxy.com:10000"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("dremoji", "i1Ack3QCS0x5hphhil")
            };
            string version = "d442197";
            //string version = API.GetVersion().Result;
            //Console.WriteLine(version);
            dynamic captcha = null;
            bool validcaptcha = false;
            string hsw = "";
            string c = "";
            string motiondata = "";
            while (!validcaptcha)
            {
                dynamic sitedata = API.CheckSiteKey(version, host, sitekey).Result;
                string nn = sitedata["c"]["req"].ToString();
                c = "{\"type\":\"hsw\",\"req\":\"" + nn + "\"}";
                hsw = API.GetHsw(nn).Result;
                motiondata = Extra.GetMotionData(website,widgetid);
                captcha = API.GetCaptcha(version,host, sitekey, c, hsw, motiondata).Result;
                if (captcha["request_type"].ToString() == "image_label_binary") { validcaptcha = true; }
            }
            //Console.WriteLine(captcha);
            string key = captcha["key"];
            string n = captcha["c"]["req"].ToString();
            c = "{\"type\":\"hsw\",\"req\":\"" + n + "\"}";
            hsw = API.GetHsw(n).Result;
            string question = captcha["requester_question"]["en"].ToString();
            string searchText = "Please click each image containing a ";
            int startIndex = question.IndexOf(searchText);
            string keyword = question.Substring(startIndex + searchText.Length);
            Dictionary<string,string> answers = new Dictionary<string,string>();
            List<Task<string[]>> tasks = new List<Task<string[]>>();
            foreach (dynamic task in captcha["tasklist"])
            {
                string[] answer = Recognition.Recognise(task, keyword);
                answers.Add(answer[0], answer[1]);
            }
            dynamic captcharesponse = API.SubmitCaptcha(version,host, sitekey, key, c, hsw, motiondata, answers).Result;
            if (captcharesponse["pass"] == true)
            {
                return captcharesponse["generated_pass_UUID"];
            }
            else
            {
                return "";
            }
        }
    }
}
