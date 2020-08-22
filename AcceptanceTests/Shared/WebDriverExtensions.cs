using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AcceptanceTests.Shared
{
    public static class WebDriverExtensions
    {
        private static readonly int waitTimeout = 10;

        public static IWebElement WaitForElement(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeout));
            return wait.Until(drv => drv.FindElement(by));
        }
    }
}
