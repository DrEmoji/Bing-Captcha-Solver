using HCaptchaSolver.Net;
using HCaptchaSolver.Net.Utils;

API.Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");
API.Client.DefaultRequestHeaders.Add("Origin", @"https://newassets.hcaptcha.com");
API.Client.DefaultRequestHeaders.Add("Referer","https://newassets.hcaptcha.com/");
string Captcha = HCaptcha.Solve("vrchat.com", "85eb5fc7-910f-44cb-b913-f92ac87596bd").Result;
Console.ReadLine();