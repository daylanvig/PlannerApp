using AcceptanceTests.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Accounts
{
    public class UsersStepBase : IDisposable
    {
        protected UserDBConnection connection;
        protected IWebDriver driver;

        [BeforeScenario]
        public void PopulateDB()
        {
            if(connection == null)
            {
                connection = new UserDBConnection();
                driver = new ChromeDriver();
            }
            connection.CleanupDB();
            connection.PopulateDB();
        }

        protected void EnterLoginDetails(string email, string password)
        {
            var loginForm = driver.FindElement(By.Id("loginForm"));
            var inputs = loginForm.FindElements(By.TagName("INPUT"));
            inputs[0].SendKeys(email);
            inputs[1].SendKeys(password);
            loginForm.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        public T ExecuteSQL<T>(string sqlText)
        {
            return connection.ExecuteScalar<T>(sqlText);
        }

        public void Dispose()
        {
            driver.Dispose();
            driver = null;
            connection.Dispose();
            connection = null;
        }
    }
}
