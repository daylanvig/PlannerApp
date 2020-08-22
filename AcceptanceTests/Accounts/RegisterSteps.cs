using System;
using AcceptanceTests.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Xunit;

namespace AcceptanceTests.Features.Accounts
{
    [Binding]
    public class RegisterSteps : IDisposable
    {

        private IWebDriver driver;
        [Given(@"that a user is on the registration page")]
        public void GivenThatAUserIsOnTheRegistrationPage()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@$"{Configuration.BASEURL}/authentication/signin");
        }

        [When(@"the user provides valid registration details")]
        public void WhenTheUserProvidesValidRegistrationDetails()
        {
            var registerForm = driver.FindElement(By.Id("registerForm"));
            var formInputs = registerForm.FindElements(By.TagName("INPUT"));
            formInputs[0].SendKeys("thisisavalidemail@testmail.com");
            formInputs[1].SendKeys("TestPassword123");
            formInputs[2].SendKeys("TestPassword123");
            var saveButton = registerForm.FindElement(By.CssSelector("button[type='submit']"));
            saveButton.Click();
        }

        [When(@"the user provides invalid registration details")]
        public void WhenTheUserProvidesInvalidRegistrationDetails()
        {
            var registerForm = driver.FindElement(By.Id("registerForm"));
            var formInputs = registerForm.FindElements(By.TagName("INPUT"));
            formInputs[0].SendKeys("daylanvig@gmail.com"); // duplicate
            formInputs[1].SendKeys("TestPassword123");
            formInputs[2].SendKeys("TestPassword123");
            var saveButton = registerForm.FindElement(By.CssSelector("button[type='submit']"));
            saveButton.Click();
        }

        [Then(@"an account should be created for them")]
        public void ThenAnAccountShouldBeCreatedForThem()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"they should be redirected to the login page")]
        public void ThenTheyShouldBeRedirectedToTheLoginPage()
        {
            driver.WaitForElement(By.Id("loginForm"));
            Assert.Contains("/authentication/signin", driver.Url);
        }

        [Then(@"an error message should be returned to them")]
        public void ThenAnErrorMessageShouldBeReturnedToThem()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"an account should not be created")]
        public void ThenAnAccountShouldNotBeCreated()
        {
            ScenarioContext.Current.Pending();
        }

        public void Dispose()
        {
            if (driver != null)
            {
                driver.Dispose();
                driver = null;
            }
        }
    }
}
