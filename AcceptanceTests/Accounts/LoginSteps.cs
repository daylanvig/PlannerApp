using AcceptanceTests.Shared;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Accounts
{
    [Binding]
    public class LoginSteps : UsersStepBase
    {
        [Given(@"that a user is on the login page")]
        public void GivenThatAUserIsOnTheLoginPage()
        {
            driver.Navigate().GoToUrl(@$"{TestConfig.BASEURL}/authentication/signin");
            driver.WaitForElement(By.Id("loginForm"), 90);
        }
        
        [When(@"the user enters valid details")]
        public void WhenTheUserEntersValidDetails()
        {
            EnterLoginDetails(TestConfig.EMAIL, TestConfig.PASSWORD);
        }
        
        [When(@"the user enter an invalid password")]
        public void WhenTheUserEnterAnInvalidPassword()
        {
            EnterLoginDetails(TestConfig.EMAIL, TestConfig.PASSWORD + "NOTPASSWORD");
        }
        
        [Then(@"they should be redirected to a list of their planner items")]
        public void ThenTheyShouldBeRedirectedToAListOfTheirPlannerItems()
        {
            driver.WaitUntil(d => d.Url.Equals(TestConfig.BASEURL, StringComparison.InvariantCultureIgnoreCase));

        }
        
        [Then(@"they should see an error message which doesn't expose whether or not an account exists")]
        public void ThenTheyShouldSeeAnErrorMessageWhichDoesnTExposeWhetherOrNotAnAccountExists()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
