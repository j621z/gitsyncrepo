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

    public class XrmCommandBarPage : XrmPage
    {
        public XrmCommandBarPage(InteractiveBrowser browser)
            : base(browser)
        {
            SwitchToDefaultContent();
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

        public BrowserCommandResult<bool> ClickCommand(string name, string subName = "", bool moreCommands = false, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Click Command"), driver => 
            {
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

                SwitchToContentFrame();
                driver.WaitForPageToLoad();
                 return true;
            });
        }      
    }
}
