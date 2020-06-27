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
            driver.WaitForElement(By.Id("plannerItems"));
            Assert.Equal("YourPlanner", driver.Title);
            
            // User sees a message notifying them that they have no planned items, with an invitation to add their first item
            var plannerArea = driver.FindElement(By.Id("plannerItems"));
            Assert.True(plannerArea.Displayed);
            Assert.Contains("You don't have anything planned. Add something above!", plannerArea.Text);

            // User enters that they have a doctor's appointment on July 2nd, 2020 at 3:00pm
            var descriptionInput = driver.FindElement(By.Id("Description"));
            var dateInput = driver.FindElement(By.Id("PlannedActionDate"));

            descriptionInput.SendKeys("Doctor's Appointment");
            // todo -> this needs to be a date field still, but need to use custom calendar
            dateInput.SendKeys("07/02/2020 15:00");

            // User submits form and sees the item appear below
            driver.FindElement(By.Id("saveNewItemBtn")).Click();

            // the form clears
            Assert.Equal("", descriptionInput.GetAttribute("value"));
            Assert.Equal("", dateInput.GetAttribute("value"));

            Assert.Contains("Doctor's Appointment", plannerArea.Text);
            Assert.Contains("July 2nd, 2020", plannerArea.Text);

            // Satisfied, user leaves the page
        }


    }
}
