using OpenQA.Selenium;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Collections.ObjectModel;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmRelatedGridPage
        : XrmPage
    {
        public XrmRelatedGridPage(InteractiveBrowser browser)
            : base(browser)
        {
            SwitchToRelatedFrame();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<Dictionary<string, Guid>> OpenViewPicker(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute("Open View Picker", driver =>
            {
                var dictionary = new Dictionary<string, Guid>();

                var viewSelectorContainer = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Grid.ViewSelectorContainer]));
                var viewLink = viewSelectorContainer.FindElement(By.TagName("a"));

                viewLink.Click();

                Thread.Sleep(500);

                var viewContainer = driver.WaitUntilAvailable(By.ClassName(Elements.CssClass[Reference.Grid.ViewContainer]));
                var viewItems = viewContainer.FindElements(By.TagName("li"));

                foreach (var viewItem in viewItems)
                {
                    if (viewItem.GetAttribute("role") != null && viewItem.GetAttribute("role") == "menuitem")
                    {
                        var links = viewItem.FindElements(By.TagName("a"));

                        if (links != null && links.Count > 1)
                        {
                            var title = links[1].GetAttribute("title");
                            Guid guid;

                            if (Guid.TryParse(viewItem.GetAttribute("id"), out guid))
                            {
                                dictionary.Add(title, guid);
                            }
                        }
                    }
                }

                return dictionary;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SwitchView(string viewName, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Switch View"), driver =>
            {
                var views = OpenViewPicker().Value;

                if (!views.ContainsKey(viewName))
                    return false;

                var viewId = views[viewName];

                // Get the LI element with the ID {guid} for the ViewId.
                var viewContainer = driver.WaitUntilAvailable(By.Id(viewId.ToString("B").ToUpper()));
                var viewItems = viewContainer.FindElements(By.TagName("a"));

                foreach (var viewItem in viewItems)
                {
                    if (viewItem.Text == viewName)
                    {
                        viewItem.Click();
                    }
                }

                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Refresh(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Refresh"), driver =>
            {                
                driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.Refresh])).Click();

                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> FirstPage(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("FirstPage"), driver =>
            {
                var firstPageIcon = driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.FirstPage]));

                if (firstPageIcon.GetAttribute("disabled") != null)
                    return false;
                else
                    firstPageIcon.Click();
                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> NextPage(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Next"), driver =>
            {
                var nextIcon = driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.NextPage]));

                if (nextIcon.GetAttribute("disabled") != null)
                    return false;
                else
                    nextIcon.Click();
                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SelectAllRecords(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("ToggleSelectAll"), driver =>
            {
                // We can check if any record selected by using
                // driver.FindElements(By.ClassName("ms-crm-List-SelectedRow")).Count == 0
                // but this function doesn't check it.
                var selectAll = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Grid.ToggleSelectAll]),
                          "The Toggle SelectAll is not available.");

                selectAll.Click();

                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> PreviousPage(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("PreviousPage"), driver =>
            {
                var previousIcon = driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.PreviousPage]));

                if (previousIcon.GetAttribute("disabled") != null)
                    return false;
                else
                    previousIcon.Click();
                return true;
            });
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Search(string searchCriteria, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Search"), driver =>
            {
                var inputs = driver.FindElements(By.TagName("input"));
                var input = inputs.Where(x => x.GetAttribute("id").Contains("findCriteria")).FirstOrDefault();

                input.SendKeys(searchCriteria);
                var searchImg = driver.FindElement(By.Id(input.GetAttribute("id") + "Img"));
                searchImg?.Click();
                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Sort(string columnName, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Sort by {columnName}"), driver =>
            {                
                var sortCols = driver.FindElements(By.ClassName(Elements.CssClass[Reference.Grid.SortColumn]));
                var sortCol = sortCols.Where(x => x.GetAttribute("fieldname") == columnName).FirstOrDefault();
                
                if (sortCol == null)
                    throw new InvalidOperationException($"Column: {columnName} Does not exist");
                else
                    sortCol.Click();
                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<List<XrmGridItem>> GetGridItems(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Get Grid Items"), driver =>
            {
                var returnList = new List<XrmGridItem>();

                var itemsTable = driver.FindElement(By.XPath(@"//*[@id=""gridBodyTable""]/tbody"));
                var columnGroup = driver.FindElement(By.XPath(@"//*[@id=""gridBodyTable""]/colgroup"));

                var rows = itemsTable.FindElements(By.TagName("tr"));

                foreach (var row in rows)
                {
                    if (!string.IsNullOrEmpty(row.GetAttribute("oid")))
                    {
                        Guid id = Guid.Parse(row.GetAttribute("oid"));
                        var link =
                            $"{new Uri(driver.Url).Scheme}://{new Uri(driver.Url).Authority}/main.aspx?etn={row.GetAttribute("otypename")}&pagetype=entityrecord&id=%7B{id:D}%7D";

                        var item = new XrmGridItem
                        {
                            EntityName = row.GetAttribute("otypename"),
                            Id = id,
                            Url = new Uri(link)
                        };

                        var cells = row.FindElements(By.TagName("td"));
                        var idx = 0;

                        foreach (var column in columnGroup.FindElements(By.TagName("col")))
                        {
                            var name = column.GetAttribute<string>("name");

                            if (!string.IsNullOrEmpty(name)
                                && column.GetAttribute("class").Contains(Elements.CssClass[Reference.Grid.DataColumn])
                                && cells.Count > idx)
                            {
                                item[name] = cells[idx].Text;
                            }

                            idx++;
                        }

                        returnList.Add(item);
                    }
                }

                return returnList;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> OpenGridRow(int index, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Open Grid Item"), driver =>
            {
                var itemsTable = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Grid.GridBodyTable]));
                var links = itemsTable.FindElements(By.TagName("a"));

                var currentIndex = 0;
                var clicked = false;

                foreach (var link in links)
                {
                    var id = link.GetAttribute("id");

                    if (id != null && id.StartsWith(Elements.ElementId[Reference.Grid.PrimaryField]))
                    {
                        if (currentIndex == index)
                        {
                            link.Click();
                            clicked = true;

                            break;
                        }

                        currentIndex++;
                    }
                }

                if (clicked)
                {
                    driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));

                    return true;
                }

                return false;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moreCommands"></param>
        /// <returns></returns>
        private BrowserCommandResult<ReadOnlyCollection<IWebElement>> GetCommands(bool moreCommands = false)
        {
            return this.Execute(GetOptions("Get Command Bar Buttons"), driver =>
            {
                IWebElement ribbon = null;
                if (moreCommands)
                    ribbon = driver.FindElement(By.XPath(Elements.Xpath[Reference.CommandBar.List]));
                else
                    ribbon = driver.FindElement(By.XPath(Elements.Xpath[Reference.CommandBar.RibbonManager]));

                var items = ribbon.FindElements(By.TagName("li"));

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

            return this.Execute(GetOptions("ClickCommand"), driver =>
            {
                if (moreCommands)
                    driver.FindElement(By.XPath(Elements.Xpath[Reference.CommandBar.MoreCommands])).Click();

                var buttons = GetCommands(moreCommands).Value;
                var button = buttons.Where(x => x.Text.Contains(name)).FirstOrDefault();

                if (button == null) { throw new Exception($"Command Button with name: {name} does not exist."); }

                if (string.IsNullOrEmpty(subName))
                    button.Click();
                else
                {
                    button.FindElement(By.ClassName(Elements.CssClass[Reference.CommandBar.FlyoutAnchorArrow])).Click();

                    var flyoutId = button.GetAttribute("id").Replace("|", "_").Replace(".", "_") + "Menu";
                    var subButtons = driver.FindElement(By.Id(flyoutId)).FindElements(By.TagName("li"));
                    subButtons.Where(x => x.Text.ToLower() == subName.ToLower()).FirstOrDefault()?.Click();
                }

                return true;
            });
        }
    }
}