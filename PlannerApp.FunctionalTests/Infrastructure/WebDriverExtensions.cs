﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.FunctionalTests.Infrastructure
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
