using BingChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HCaptchaSolver.Net.Utils
{
    internal class Bing
    {
        public static async Task<string[]> Recognise(dynamic task, string keyword, WebProxy Proxy)
        {
            string imagelink = task["datapoint_uri"].ToString();
            string taskkey = task["task_key"].ToString();
            try
            {
                var client = new BingChatClient(new BingChatClientOptions
                {
                    Tone = BingChatTone.Precise,
                    CookieU = "13AKJ7QRPswh9SVjzhRmv95ZLsofckw7ZiDOcfqKBYs44Z4rs7SEMU7DNyBxsJvIHhsU5XmOCckqdHd1k6SWT_kRh2b-xbEnMWUC8b3fZmI6adwSgqUAUgKn-pu2cGc8Cg6C7_VkMEjvimVkMW1vtd9aHlo4aQar2WSi79_h7Tsxs0CrA6fQDdWERZdYesHHm6QNhl2bdu9XOwBoIbcc2h0sIDoGXjUZ1XIAXE228V1g",
                    CookieKievRPSSecAuth = "FAB6BBRaTOJILtFsMkpLVWSG6AN6C/svRwNmAAAEgAAACJ3EesynrFSUOATnH19Obn7eLdDA7Y8xh6HSDrA1KcCF7Csq88CmhpkSIuug+c8N8se0d57toMEAnvWBji2aVgSAEvCxz2JbjeYQKfCCKO3p1IzuNri48rD8UZuisoW38RECbPHnPBXaIMqDjE5Q1iq0r1prEXdhVpxRHBHafKlVhpjtKtSjH2khXpPQNgrllDzajOJ1J9w4VcQ3SOqrB3LH17MMAw7twEvRIdxlbR941JBhhjlIF20vPnb5hAHbvW72gKBccmsP7R6Fnq7Ew8va47m8vaHACqn/Lm4v9ylmLCXGSA4cNbLg8DFK0PcOr0pSKW2qHgZCuRH110aEJXYf7pADr8g550HtJJ2XZgUhEjnewCczWKuCxzGN3a2VVcafDMj5QaHkDgekNHZbJTiC9I8+J8Sv31Aj8LjNZzRkArpYhF7SNYq9D2lqqAs1Bsk5megbO7LAYKezqUjwxo+QuArsXElVNMs8Ma0zlr5DfJSwQEbV3wo9Ee4mCywVsXhlysFkPmSA1iSkhr3cajm5c+A+67+O/iXQpF1aX5XH9e4bxxZlW1ZYmOb0Dz9mhTAs94mqTYoW0rzTUyCwHZrppYUaXwjb8oWYIL/o0v5giUMN5qn2pIGfyyHYMcGbTRqU0i+cqCbhNW2WdLhmvlO8CiMwFxjCBRxdja/M5lvFCl3RktQhTDNO4kNZvlaxclBymypTA7CnJbFAEB/gIYeqOjSI/UOWCrwuyGUKQbSVVvCUYgC5/3IrojSekEFmefaJCWPZpxymWq4m7UxjbKgFTxIi2lIWbuja/IAJS4TaE1Gg7/RuYQCv+gAm+N5W1G6hhRWH2pmzhYCaBaLCowoQIhUj4SjSAJt/t9gFXlzb6Q/62Z1WJmnUnD2sQWJwD8jXATUCfpx1T22kRNErtaru7OFh4C3ow1HMKdTz+fxEVN6ucPbGFeEGfQOT34BmAnxLoWEPXiOUjsxaM2XnOkcCm7zxykiJdNhW8XxdWVpbo/OFHuJVjOLZmHqjWPK3i69RNGnJUy6Gc17nOGTQDNzkviQozgnJsFts+mJuDzFORJXDWNqRptZaz3usxPFJxefKSNTdK5Md/L7ItReAOijudPforR3fxU915X+/La8DG9yzXJAvfHylKKR4TOhxSl/vpiVtGfxbObQOSMPmA5XzBQuspD3XAcBq/Tb8KjU8Wrcc89+BC0fCQkfnT84TkGOgvR3ysaE/UOPABjlrjCEecjRy1mQmTiQGBciwtkYBXtzm/KtNk7BToNy35sDrVuw4+xtagSC4T4JXFHXel0njAY7Dd2ro/RARXeG0jUTKzCFtRQhIVA31KtzbJZ+M+o3Qvg5eDlZBQBfHBHE7+C9hnHsAEX4MAkTry443rT1ziH5V2dvoxfxllcBIgI3vlDBpBnYtbabwPWGncOJydnZ99ay17YS7rxXcCTCrKu5S1M4UAP2CCJJ0xIfsJTn35eZCmbnIjEQA"

                });
                var message = $"Go into detail in what this object is Dont provide links, if it contains object {keyword} then please mention it however if it doesnt contain the object dont include the object name in response";
                string result = "false";
                var answer = await client.AskAsync(message, default, Proxy, imagelink);
                if (answer.ToLower().Contains(keyword.ToLower()))
                {
                    result = "true";
                }
                Console.WriteLine($"{imagelink} is done");
                return new string[] { imagelink, taskkey, result };
            }
            catch
            {
                return new string[] { imagelink, taskkey, "manual" };
            }
        }
    }
}
