using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace PlannerApp.FunctionalTests
{
    public class PlannerAppUserShould : IDisposable
    {
        private const string rootUrl = "https://localhost:44353/";
        public IWebDriver driver;

        public PlannerAppUserShould()
        {
            driver = new ChromeDriver();
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void BeAbleToAddAnItem()
        {
            // User goes to homepage of application
            driver.Navigate().GoToUrl(rootUrl);
            Assert.Equal("YourPlanner", driver.Title);

            // User sees a message notifying them that they have no planned items, with an invitation to add their first item

            // User enters a quick description of the task, and the date/time they wish to do it

            // User submits form and sees the item appear below

            // Satisfied, user leaves the page
        }


    }
}
