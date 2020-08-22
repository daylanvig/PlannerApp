using OpenQA.Selenium;
using System;
using System.Threading;

namespace AcceptanceTests.Shared
{
    public class HelperMethods
    {
        private static readonly int waitTimeout = 10;

        public static void WaitForCondition(Func<bool> condition, int? timeout = null)
        {
            var waitTime = timeout ?? waitTimeout;
            var latestEndTime = DateTime.Now.AddSeconds(waitTime);
            while (DateTime.Now < latestEndTime)
            {
                // check if condition is true, if so exit
                var isDone = condition();
                if (isDone)
                {
                    return;
                }
                // otherwise wait 500 ms
                Thread.Sleep(500);
            }
            throw new TimeoutException("Condition was not true in allowed timespan");
        }

        public static void EnterDateTimeFieldValue(IWebElement element, string dateString)
        {
            var dateTime = dateString.Split(" ");
            element.SendKeys(dateTime[0]);
            element.SendKeys(Keys.ArrowRight);
            element.SendKeys(dateTime[1]);
            element.SendKeys(dateTime[2]);
        }
    }
}
