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
        /// 
        /// </summary>
        /// <param name="moreCommands"></param>
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
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subName"></param>
        /// <param name="moreCommands"></param>
        /// <param name="thinkTime"></param>
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
