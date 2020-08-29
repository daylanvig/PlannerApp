using System;
using System.Configuration;
using System.Data.SqlClient;
using AcceptanceTests.Accounts;
using AcceptanceTests.Shared;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Xunit;

namespace AcceptanceTests.Features.Accounts
{
    [Binding]
    public class RegisterSteps : UsersStepBase
    {
        private const string VALID_EMAIL = "thisisavalidemail@testmail.com";
        private const string INVALID_EMAIL = "duplicateemail@testmail.com";
        private const string PASSWORD = "Password123*";
        
        [Given(@"that a user is on the registration page")]
        public void GivenThatAUserIsOnTheRegistrationPage()
        {   
            driver.Navigate().GoToUrl(@$"{TestConfig.BASEURL}/authentication/register");
            driver.WaitForElement(By.Id("registerForm"), 90);
        }

        [When(@"the user provides valid registration details")]
        public void WhenTheUserProvidesValidRegistrationDetails()
        {
            var registerForm = driver.FindElement(By.Id("registerForm"));
            var formInputs = registerForm.FindElements(By.TagName("INPUT"));
            formInputs[0].SendKeys(VALID_EMAIL);
            formInputs[1].SendKeys(PASSWORD);
            formInputs[2].SendKeys(PASSWORD);
            var saveButton = registerForm.FindElement(By.CssSelector("button[type='submit']"));
            saveButton.Click();
        }

        [When(@"the user provides invalid registration details")]
        public void WhenTheUserProvidesInvalidRegistrationDetails()
        {
            var registerForm = driver.FindElement(By.Id("registerForm"));
            var formInputs = registerForm.FindElements(By.TagName("INPUT"));
            formInputs[0].SendKeys(INVALID_EMAIL); // duplicate
            formInputs[1].SendKeys(PASSWORD);
            formInputs[2].SendKeys(PASSWORD);
            var saveButton = registerForm.FindElement(By.CssSelector("button[type='submit']"));
            saveButton.Click();
        }

        [Then(@"an account should be created for them")]
        public void ThenAnAccountShouldBeCreatedForThem()
        {
            var script = $"SELECT Count(*) FROM [USER] where Email like '{VALID_EMAIL}'";
            Assert.Equal(1, ExecuteSQL<int>(script));
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
            driver.WaitForElement(By.CssSelector("#registerForm .help.is-danger .validation-message"));
            var errorSpan = driver.FindElement(By.CssSelector("#registerForm .help.is-danger .validation-message"));
            Assert.Contains("Username 'duplicateemail@testmail.com' is already taken.", errorSpan.Text);
        }

        [Then(@"an account should not be created")]
        public void ThenAnAccountShouldNotBeCreated()
        {
            var script = $"SELECT Count(*) FROM [USER] where Email like '{INVALID_EMAIL}' and TenantID != '54e7f8ef-e3de-45a1-94ca-32951cc99401'"; // this tenantID is known to be for an already existing account that uses this email
            Assert.Equal(0, ExecuteSQL<int>(script));
        }
    }
}
