using OpenQA.Selenium;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.Dynamics365.UITests.Api.XrmPage" />
    public class XrmGridPage
        : XrmPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XrmGridPage"/> class.
        /// </summary>
        /// <param name="browser">The browser.</param>
        public XrmGridPage(InteractiveBrowser browser)
            : base(browser)
        {
            SwitchToContentFrame();
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

                var viewSelectorContainer = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Grid.ControlBar]));
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
                                //Handle Duplicate View Names
                                //Temp Fix
                                if(!dictionary.ContainsKey(title))
                                    dictionary.Add(title, guid);
                            }
                        }
                    }
                }

                return dictionary;
            });
        }

        /// <summary>
        /// Switches the view.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="thinkTime">The think time.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SwitchView(string viewName, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Switch View"), driver =>
            {
                var views = OpenViewPicker().Value;

                if (!views.ContainsKey(viewName))
                {
                    throw new InvalidOperationException($"No view with the name '{viewName}' exists.");
                }

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
        /// Refreshes this instance.
        /// </summary>
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
        /// Firsts the page.
        /// </summary>
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
        /// Nexts the page.
        /// </summary>
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
        /// Toggles the select all.
        /// </summary>
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
        /// Previouses the page.
        /// </summary>
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
        /// Opens the chart.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<bool> OpenChart(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("OpenChart"), driver =>
            {
                driver.FindElement(By.ClassName(Elements.CssClass[Reference.Grid.OpenChart])).Click();

                return true;
            });
        }

        /// <summary>
        /// Closes the chart.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<bool> CloseChart(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("CloseChart"), driver =>
            {
                driver.FindElement(By.ClassName(Elements.CssClass[Reference.Grid.CloseChart])).Click();

                return true;
            });
        }

        /// <summary>
        /// Pins this instance.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<bool> Pin(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Pin"), driver =>
            {
                driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.DefaultViewIcon])).Click();

                return true;
            });
        }

        /// <summary>
        /// Searches the specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Search(string searchCriteria, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Search"), driver =>
            {
                driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.FindCriteria])).SendKeys(searchCriteria);
                driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.FindCriteriaImg])).Click();

                return true;
            });
        }

        /// <summary>
        /// Sorts the specified column name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Sort(string columnName,int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Sort by {columnName}"), driver =>
            {
                var sortCols = driver.FindElements(By.ClassName(Elements.CssClass[Reference.Grid.SortColumn]));
                var sortCol = sortCols.FirstOrDefault(x => x.Text == columnName);
                if (sortCol == null)
                    throw new InvalidOperationException($"Column: {columnName} Does not exist");
                else
                    sortCol.Click();
                return true;
            });
        }

        /// <summary>
        /// Gets the grid items.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<List<XrmGridItem>> GetGridItems()
        {
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
        /// Opens the grid record.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> OpenRecord(int index,int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Open Grid Record"), driver =>
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
                    //driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));
                    driver.WaitForPageToLoad();
                    return true;
                }
               else
               {
                   throw new InvalidOperationException($"No record with the index '{index}' exists.");
                }
            });
        }

        /// <summary>
        /// Selects the grid record.
        /// </summary>
        /// <param name="index">The index of the row you want to select. Index starts with 0.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SelectRecord(int index, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Select Grid Record"), driver =>
            {
                //index parameter will be 0 based but the Xpath is 1 based. So we need to increment.
                index++;

                var select = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Grid.RowSelect].Replace("[INDEX]", index.ToString())),
                                                        $"Row with index {index.ToString()} is not found");

                select?.Click();
                
                return false;
            });
        }

        /// <summary>
        /// Filters the by letter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Filter criteria is not valid.</exception>
        public BrowserCommandResult<bool> FilterByLetter(char filter, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            if (!Char.IsLetter(filter) && filter != '#')
                throw new InvalidOperationException("Filter criteria is not valid.");

            return this.Execute(GetOptions("Filter by Letter"), driver =>
            {
                var jumpBar = driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.JumpBar]));
                var letterCells = jumpBar.FindElements(By.TagName("TD"));

                foreach (var letter in letterCells)
                {
                    if (letter.Text == filter.ToString())
                        letter.Click();
                }
               
                return true;
            });
        }

        /// <summary>
        /// Filters the by all.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<bool> FilterByAll(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Filter by All Records"), driver =>
            {
                var showAll = driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.ShowAll]));

                showAll?.Click();

                return true;
            });
        }

        /// <summary>
        /// Opens the filter.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<bool> EnableFilter(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Enable Filter"), driver =>
            {
                var filter = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Grid.Filter]),
                                                        "Filter option is not available");

                filter?.Click();

                return true;
            });
        }

        /// <summary>
        /// Switches the chart on the Grid.
        /// </summary>
        /// <param name="chartName">Name of the chart.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SwitchChart(string chartName, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            if (!Browser.Driver.IsVisible(By.XPath(Elements.Xpath[Reference.Grid.ChartList])))
                OpenChart();

            Browser.ThinkTime(1000);

            return this.Execute(GetOptions("Switch Chart"), driver =>
            {
                driver.ClickWhenAvailable(By.XPath(Elements.Xpath[Reference.Grid.ChartList]));

                var dialog = driver.FindElement(By.XPath(Elements.Xpath[Reference.Grid.ChartDialog]));
                var menuItems = dialog.FindElements(By.TagName("a"));
                IWebElement selectedItem = null;

                foreach (var item in menuItems)
                {
                    if (item.GetAttribute("title") == chartName)
                        selectedItem = item;
                }

                if (selectedItem == null)
                    throw new InvalidOperationException($"Chart with name {chartName} does not exist");
                else
                    selectedItem.Click();

                return true;
            });
        }

    }
}