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
        public static async Task<string> Solve(string host, string sitekey)
        {
            WebProxy Proxy = new WebProxy
            {
                Address = new Uri("http://gb.smartproxy.com:30000"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("dremoji", "i1Ack3QCS0x5hphhil")
            };
            dynamic sitedata = API.CheckSiteKey(host, sitekey).Result;
            string n = sitedata["c"]["req"].ToString();
            string c = "{\"type\":\"hsw\",\"req\":\"" + n + "\"}";
            string hsw = API.GetHsw(n).Result;
            string motiondata = Extra.GetMotionData("https://vrchat.com/home/register", "0gdec1jq6oa");
            dynamic captcha = API.GetCaptcha(host, sitekey, c, hsw, motiondata).Result;
            Console.WriteLine(captcha);
            string key = captcha["key"];
            string question = captcha["requester_question"]["en"].ToString();
            string searchText = "Please click each image containing a ";
            int startIndex = question.IndexOf(searchText);
            string keyword = question.Substring(startIndex + searchText.Length);
            Dictionary<string,string> answers = new Dictionary<string,string>();
            List<Task<string[]>> tasks = new List<Task<string[]>>();
            foreach (dynamic task in captcha["tasklist"])
            {
                string link = task["datapoint_uri"];
                string valid = "false";
                Task<string[]> recognitionTask = Bing.Recognise(task, keyword, Proxy);
                tasks.Add(recognitionTask);
            }
            await Task.WhenAll(tasks);
            foreach (Task<string[]> recognitionTask in tasks)
            {
                string[] answer = recognitionTask.Result;
                Console.WriteLine($"{answer[0]} - {answer[2]}");
                if (answer[2] == "manual")
                {
                    Console.WriteLine(answer[0]);
                    Console.Write($"Is this {keyword}: ");
                    answers.Add(answer[1], Console.ReadLine());


                }
                else
                {
                    Console.WriteLine($"{answer[0]} - {answer[2]}");
                    answers.Add(answer[1], answer[2]);
                }
            }
            dynamic captcharesponse = API.SubmitCaptcha(host, sitekey, key, c, hsw, motiondata, answers).Result;
            Console.WriteLine(captcharesponse);
            return "";
        }
    }
}
