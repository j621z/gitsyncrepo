using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;
using System.Threading;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmCommandBarItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class XrmCommandBarPage : BrowserPage
    {
        public XrmCommandBarPage(InteractiveBrowser browser)
            : base(browser)
        {
        }
              
        internal BrowserCommandOptions GetOptions()
        {
            return new BrowserCommandOptions(Constants.DefaultTraceSource,
                "Ribbon command",
                0,
                2000,
                null,
                false,
                typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        private BrowserCommandResult<ReadOnlyCollection<IWebElement>> GetCommands(bool moreCommands = false)
        {
            return this.Execute("Get Command Bar Buttons", driver =>
            {
                driver.WaitUntilAvailable(By.Id("crmRibbonManager"),new TimeSpan(0,0,5));

                IWebElement ribbon = null;
                if (moreCommands)
                    ribbon = driver.FindElement(By.Id("moreCommandsList"));
                else
                    ribbon = driver.FindElement(By.Id("crmRibbonManager"));

                var items = ribbon.FindElements(By.TagName("li"));

                return items;//.Where(item => item.Text.Length > 0).ToDictionary(item => item.Text, item => item.GetAttribute("id"));
            });
        }

        public BrowserCommandResult<bool> ClickCommand(string name, string subName = "", bool moreCommands = false)
        {
            //return this.Execute(GetOptions(), this.ClickCommandButton, name);
            Thread.Sleep(4000);
            return this.Execute(GetOptions(), driver => 
            {
                driver.SwitchTo().DefaultContent();

                if (moreCommands)
                    driver.FindElement(By.Id("moreCommands")).Click();

                var buttons = GetCommands(moreCommands).Value;
                var button = buttons.Where(x => x.Text.ToLower() == name.ToLower()).FirstOrDefault();

                if (string.IsNullOrEmpty(subName))
                    button.Click();
                else
                {
                    button.FindElement(By.ClassName("flyoutAnchorArrow")).Click();

                    var flyoutId = button.GetAttribute("id").Replace("|", "_").Replace(".", "_") + "Menu";
                    var subButtons = driver.FindElement(By.Id(flyoutId)).FindElements(By.TagName("li"));
                    subButtons.Where(x => x.Text.ToLower() == subName.ToLower()).FirstOrDefault()?.Click();
                }

                driver.WaitForPageToLoad();
                 return true;
            });
        }      
    }
}
