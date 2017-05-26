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


        /// <summary>
        /// Gets the Commands
        /// </summary>
        /// <param name="moreCommands">The moreCommands</param>
        /// <returns></returns>
        private BrowserCommandResult<ReadOnlyCollection<IWebElement>> GetCommands(bool moreCommands = false)
        {
            return this.Execute("Get Command Bar Buttons", driver =>
            {
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.CommandBar.RibbonManager]),new TimeSpan(0,0,5));

                IWebElement ribbon = null;
                if (moreCommands)
                    ribbon = driver.FindElement(By.XPath(Elements.Xpath[Reference.CommandBar.List]));
                else
                    ribbon = driver.FindElement(By.XPath(Elements.Xpath[Reference.CommandBar.RibbonManager]));

                var items = ribbon.FindElements(By.TagName("span"));

                return items;//.Where(item => item.Text.Length > 0).ToDictionary(item => item.Text, item => item.GetAttribute("id"));
            });
        }

        /// <summary>
        /// Clicks the  Command
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="subName">The subName</param>
        /// <param name="moreCommands">The moreCommands</param>
        /// <param name="thinkTime">The thinkTime</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> ClickCommand(string name, string subName = "", bool moreCommands = false, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Click Command"), driver => 
            {
                if (moreCommands)
                    driver.FindElement(By.XPath(Elements.Xpath[Reference.CommandBar.MoreCommands])).Click();

                var buttons = GetCommands(moreCommands).Value;
                var button = buttons.Where(x => x.Text.ToLower() == name.ToLower()).FirstOrDefault();

                if (string.IsNullOrEmpty(subName))
                {
                    if (button != null)
                    {
                        button.Click();
                    }
                    else
                    {
                        throw new InvalidOperationException($"No command with the name '{name}' exists inside of Commandbar.");
                    }
                }
                    
                else
                {

                    button.FindElement(By.ClassName(Elements.CssClass[Reference.CommandBar.FlyoutAnchorArrow])).Click();

                    var subButtons = driver.FindElements(By.ClassName("ms-crm-Menu-Label"));
                    var item = subButtons.Where(x => x.Text.ToLower() == subName.ToLower()).FirstOrDefault();
                    if(item == null) { throw new InvalidOperationException($"The sub menu item '{subName}' is not found."); }

                    item.Click();
                }
                
                driver.WaitForPageToLoad();
                 return true;
            });
        }      
    }
}
