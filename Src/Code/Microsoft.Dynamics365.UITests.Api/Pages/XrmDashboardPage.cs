using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;
using System.Threading;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmDashboardPage
        : XrmPage
    {
        public XrmDashboardPage(InteractiveBrowser browser)
            : base(browser)
        {
            SwitchToContentFrame();
        }


        /// <summary>
        /// Switches between the DashBoard.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<bool> SelectDashBoard(string dashBoardName, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Select Dashboard"), driver =>
            {
                Dictionary<string, System.Guid> dashboards = OpenViewPicker();

                if (!dashboards.ContainsKey(dashBoardName))
                    throw new InvalidOperationException($"Dashboard '{dashBoardName}' does not exist in the dashboard select options");

                var viewId = dashboards[dashBoardName];

                // Get the LI element with the ID {guid} for the ViewId.
                var viewContainer = driver.WaitUntilAvailable(By.Id(viewId.ToString("B")));
                var viewItems = viewContainer.FindElements(By.TagName("a"));

                foreach (var viewItem in viewItems)
                {
                    if (viewItem.Text == dashBoardName)
                    {
                        viewItem.Click();
                    }
                }

                return true;
            });
        }

        /// <summary>
        /// Opens the view picker.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<Dictionary<string, Guid>> OpenViewPicker(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute("Open View Picker", driver =>
            {
                var dictionary = new Dictionary<string, Guid>();

                var dashboardSelectorContainer = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.DashBoard.Selector]));
                dashboardSelectorContainer.FindElement(By.TagName("a")).Click();

                //Handle Firefox not clicking the viewpicker the first time
                driver.WaitUntilVisible(By.ClassName(Elements.CssClass[Reference.DashBoard.ViewContainerClass]),
                                        new TimeSpan(0, 0, 2),
                                        null,
                                        d => { dashboardSelectorContainer.FindElement(By.TagName("a")).Click(); });

                var viewContainer = driver.WaitUntilAvailable(By.ClassName(Elements.CssClass[Reference.DashBoard.ViewContainerClass]));
                var viewItems = viewContainer.FindElements(By.TagName("li"));

                foreach (var viewItem in viewItems)
                {
                    if (viewItem.GetAttribute("role") != null && viewItem.GetAttribute("role") == "menuitem")
                    {
                        var links = viewItem.FindElements(By.TagName("a"));

                        if (links != null && links.Count > 1)
                        {
                            //var title = links[1].GetAttribute("title");
                            var tag = links[1].FindElement(By.TagName("nobr"));
                            var name = tag.Text;
                            Guid guid;

                            if (Guid.TryParse(viewItem.GetAttribute("id"), out guid))
                            {
                                //Handle Duplicate View Names
                                //Temp Fix
                                if (!dictionary.ContainsKey(name))
                                    dictionary.Add(name, guid);
                            }
                        }
                    }
                }

                return dictionary;
            });
        }
    }
}