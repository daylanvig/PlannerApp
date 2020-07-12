using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PlannerApp.FunctionalTests.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PlannerApp.FunctionalTests
{
    public class PlannerAppUserShould : IDisposable
    {
        private const string rootUrl = "http://localhost:44328/";
        public IWebDriver driver;

        public PlannerAppUserShould()
        {
            driver = new ChromeDriver();
        }

        public void Dispose()
        {
            driver.Quit();
        }

        private IWebElement GetDescriptionInput() => driver.FindElement(By.Name("Description"));
        private IWebElement GetDateInput() => driver.FindElement(By.Name("PlannedActionDate"));
        private IWebElement GetEndDateInput() => driver.FindElement(By.Name("PlannedEndDate"));
        private bool HasLoadedUsersItems()
        {
            var plannerArea = driver.FindElement(By.Id("plannerItems"));
            return !plannerArea.Text.Contains("Loading");
        }

        private int GetNumberOfItems() => driver.FindElements(By.CssSelector("[id^='item-'")).Count;
        private void WaitForItemToBeAdded(int initialNumberOfItems)
        {
            bool isItemAdded()
            {
                return GetNumberOfItems() > initialNumberOfItems;
            }
            HelperMethods.WaitForCondition(isItemAdded);
        }

        [Fact]
        public void BeAbleToAddAnItem()
        {
            // User goes to homepage of application
            driver.Navigate().GoToUrl(rootUrl);
            driver.WaitForElement(By.Id("plannerItems"));
            HelperMethods.WaitForCondition(HasLoadedUsersItems);

            // User sees a message notifying them that they have no planned items, with an invitation to add their first item
            var plannerArea = driver.FindElement(By.Id("plannerItems"));
            Assert.True(plannerArea.Displayed);
            Assert.Contains("You don't have anything planned. Add something above!", plannerArea.Text);

            // User enters that they have a doctor's appointment on July 2nd, 2020 at 3:00pm
            GetDescriptionInput().SendKeys("Doctor's Appointment");
            // todo -> this needs to be a date field still, but need to use custom calendar
            
            HelperMethods.EnterDateTimeFieldValue(GetDateInput(), "07/02/2020 3:00 PM");
            HelperMethods.EnterDateTimeFieldValue(GetEndDateInput(), "07/02/2020 3:30 PM");
            var currentCount = GetNumberOfItems();
            // User submits form and sees the item appear below
            driver.FindElement(By.Id("saveNewItemBtn")).Click();

            WaitForItemToBeAdded(currentCount);
            // the form clears
            Assert.Equal("", GetDescriptionInput().GetAttribute("value"));
            Assert.Equal("", GetDateInput().GetAttribute("value"));

            Assert.Contains("Doctor's Appointment", plannerArea.Text);
            Assert.Contains("July 2, 2020", plannerArea.Text);

            // Satisfied, user leaves the page
        }

        [Fact]
        public void BeAbleToDeleteAnItem()
        {
            // User goes to homepage of application
            driver.Navigate().GoToUrl(rootUrl);
            driver.WaitForElement(By.Id("plannerItems"));
            HelperMethods.WaitForCondition(HasLoadedUsersItems);

            // User sees a message notifying them that they have no planned items, with an invitation to add their first item
            var plannerArea = driver.FindElement(By.Id("plannerItems"));
            Assert.True(plannerArea.Displayed);
            Assert.Contains("You don't have anything planned. Add something above!", plannerArea.Text);

            // User enters that they have a doctor's appointment on July 2nd, 2020 at 3:00pm
            GetDescriptionInput().SendKeys("Doctor's Appointment");
            // todo -> this needs to be a date field still, but need to use custom calendar
            HelperMethods.EnterDateTimeFieldValue(GetDateInput(), "07/02/2020 3:00 PM");
            HelperMethods.EnterDateTimeFieldValue(GetEndDateInput(), "07/02/2020 3:30 PM");
            var currentCount = GetNumberOfItems();
            // User submits form and sees the item appear below
            driver.FindElement(By.Id("saveNewItemBtn")).Click();
            WaitForItemToBeAdded(currentCount);
            var items = driver.FindElements(By.CssSelector("[id^=item-]"));
            var newItem = items.First();
            var itemElementId = newItem.GetAttribute("id");
            newItem.FindElement(By.ClassName("delete")).Click();
            Thread.Sleep(2000); // TODO: HACK
            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.Id(itemElementId)));
        }


    }
}
