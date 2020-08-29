using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AcceptanceTests.Shared
{
    public static class WebDriverExtensions
    {
        private const int DEFAULT_WAIT_TIME = 15; // Seconds
        public static void LoginToTestAccount(this IWebDriver driver)
        {
            driver.Navigate().GoToUrl(@$"{TestConfig.BASEURL}/authentication/signin");
            driver.WaitForElement(By.Id("loginForm"), 90);
            var loginForm = driver.FindElement(By.Id("loginForm"));
            var inputs = loginForm.FindElements(By.TagName("INPUT"));
            inputs[0].SendKeys(TestConfig.EMAIL);
            inputs[1].SendKeys(TestConfig.PASSWORD);
            loginForm.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        public static IWebElement WaitForElement(this IWebDriver driver, By by, int waitTimeout = DEFAULT_WAIT_TIME)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeout));
            return wait.Until(drv => drv.FindElement(by));
        }

        public static TResult WaitUntil<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> condition, int waitTimeout = DEFAULT_WAIT_TIME)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeout));
            return wait.Until(condition);
        }
    }
}
