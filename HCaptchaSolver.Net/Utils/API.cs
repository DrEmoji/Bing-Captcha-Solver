﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Clarifai.Api.V2;

namespace HCaptchaSolver.Net.Utils
{
    internal class API
    {
        public static async Task<dynamic> CheckSiteKey(HttpClient Client, string version, string host, string sitekey)
        {
            HttpResponseMessage response = await Client.PostAsync($"https://api2.hcaptcha.com/checksiteconfig?v={version}&host={host}&sitekey={sitekey}&sc=1&swa=1&spst=0", null);
            return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
        }

        public static async Task<string> GetHsw(HttpClient Client, string req)
        {
            string payload = JsonConvert.SerializeObject(new
            {
                script = "https://newassets.hcaptcha.com/c/278beb8b/hsw.js",
                req = req
            });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync(new Uri("https://hcaptcha.vxxx.cf/hsw"), content);
            return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result)["result"];
        }

        public static async Task<dynamic> GetCaptcha(HttpClient Client, string version, string host, string sitekey, string c, string n, string motiondata)
        {

            var regdata = new[]
            {   
                new KeyValuePair<string, string>("v",version),
                new KeyValuePair<string, string>("sitekey", sitekey),
                new KeyValuePair<string, string>("host", host),
                new KeyValuePair<string, string>("hl", "en"),
                new KeyValuePair<string, string>("c", c),
                new KeyValuePair<string, string>("n", n),
                new KeyValuePair<string, string>("motionData", motiondata)
            };  

            HttpResponseMessage response = await Client.PostAsync($"https://hcaptcha.com/getcaptcha/{sitekey}", new FormUrlEncodedContent(regdata));
            return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
        }

        public static async Task<dynamic> SubmitCaptcha(HttpClient Client, string version, string host, string sitekey, string key, string c, string n, string motiondata, Dictionary<string, string> answers)
        {
            string link = $"https://hcaptcha.com/checkcaptcha/{sitekey}/{key}";
            string payload = JsonConvert.SerializeObject(new
            {
                answers = answers,
                c = c,
                job_mode = "image_label_binary",
                n = n,
                serverdomain = host,
                motionData = motiondata,
                sitekey = sitekey,
                v = version
            });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync(link, content);
            return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
