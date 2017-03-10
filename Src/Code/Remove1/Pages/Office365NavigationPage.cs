using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public class Office365NavigationPage
        : BrowserPage
    {
        public Office365NavigationPage(InteractiveBrowser browser)
            : base(browser)
        {
        }

        private static BrowserCommandOptions NavigationRetryOptions
        {
            get
            {
                return new BrowserCommandOptions(
                    Constants.DefaultTraceSource,
                    "Open Waffle Menu",
                    5,
                    1000,
                    null,
                    false, 
                    typeof(StaleElementReferenceException));
            }
        }

        public BrowserCommandResult<Dictionary<string, Uri>> OpenWaffleMenu()
        {
            return this.Execute(NavigationRetryOptions, driver =>
            {
                var dictionary = new Dictionary<string, Uri>();

                driver.ClickWhenAvailable(By.Id("O365_MainLink_NavMenu"));

                var element = driver.FindElement(By.ClassName("o365cs-nav-navMenuTabContainer"));
                var subItems = element.FindElements(By.ClassName("o365cs-nav-appItem"));

                foreach (var subItem in subItems)
                {
                    var link = subItem.FindElement(By.TagName("a"));

                    if (link != null)
                    {
                        dictionary.Add(link.Text, new Uri(link.GetAttribute("href")));
                    }
                }

                return dictionary;
            });
        }
    }
}