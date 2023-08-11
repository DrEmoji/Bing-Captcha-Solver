using HCaptchaSolver.Net;
using HCaptchaSolver.Net.Utils;
using System.Diagnostics;

while (true)
{
    string Captcha = HCaptcha.Solve("https://vrchat.com/home/register", "0gdec1jq6oa", "vrchat.com", "85eb5fc7-910f-44cb-b913-f92ac87596bd").Result;
    Console.WriteLine(Captcha);
}
