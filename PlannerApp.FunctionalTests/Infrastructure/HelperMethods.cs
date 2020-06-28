using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlannerApp.FunctionalTests.Infrastructure
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
    }
}
