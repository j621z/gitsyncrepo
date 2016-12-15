using OpenQA.Selenium;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Xrm.UITests.Api
{
    public class XrmGridPage
        : BrowserPage
    {
        public XrmGridPage(InteractiveBrowser browser)
            : base(browser)
        {
        }

        internal BrowserCommandOptions GetOptions(string commandName)
        {
            return new BrowserCommandOptions(Constants.DefaultTraceSource,
                commandName,
                0,
                0,
                null,
                false,
                typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        public BrowserCommandResult<Dictionary<string, Guid>> OpenViewPicker()
        {
            return this.Execute("Open View Picker", driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var dictionary = new Dictionary<string, Guid>();

                var viewSelectorContainer = driver.WaitUntilAvailable(By.Id("gridControlBar"));
                var viewLink = viewSelectorContainer.FindElement(By.TagName("a"));

                viewLink.Click();

                Thread.Sleep(500);

                var viewContainer = driver.WaitUntilAvailable(By.ClassName("ms-crm-VS-Menu"));
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

        public BrowserCommandResult<bool> SwitchView(string viewName)
        {
            return this.Execute(GetOptions("Switch View"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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

        public BrowserCommandResult<bool> Refresh()
        {
            return this.Execute(GetOptions("Refresh"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();
                
                driver.FindElement(By.Id("grid_refresh")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> FirstPage()
        {
            return this.Execute(GetOptions("FirstPage"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var firstPageIcon = driver.FindElement(By.Id("fastRewind"));

                if (firstPageIcon.GetAttribute("disabled") != null)
                    return false;
                else
                    firstPageIcon.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> NextPage()
        {
            return this.Execute(GetOptions("Next"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var nextIcon = driver.FindElement(By.Id("_nextPageImg"));

                if (nextIcon.GetAttribute("disabled") != null)
                    return false;
                else
                    nextIcon.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> ToggleSelectAll()
        {
            return this.Execute(GetOptions("ToggleSelectAll"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                // We can check if any record selected by using
                // driver.FindElements(By.ClassName("ms-crm-List-SelectedRow")).Count == 0
                // but this function doesn't check it.

                driver.FindElement(By.Id("chkAll"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> PreviousPage()
        {
            return this.Execute(GetOptions("PreviousPage"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var previousIcon = driver.FindElement(By.Id("_prevPageImg"));

                if (previousIcon.GetAttribute("disabled") != null)
                    return false;
                else
                    previousIcon.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> OpenChart()
        {
            return this.Execute(GetOptions("OpenChart"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.ClassName("ms-crm-ImageStrip-navLeft")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> CloseChart()
        {
            return this.Execute(GetOptions("CloseChart"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.ClassName("ms-crm-PaneChevron")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Pin()
        {
            return this.Execute(GetOptions("Pin"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id("defaultViewIcon")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Search(string searchCriteria)
        {
            return this.Execute(GetOptions("Search"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id("crmGrid_findCriteria")).SendKeys(searchCriteria);
                driver.FindElement(By.Id("crmGrid_findCriteriaImg")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Sort(string columnName)
        {
            return this.Execute(GetOptions($"Sort by {columnName}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var sortCols = driver.FindElements(By.ClassName("ms-crm-List-Sortable"));
                var sortCol = sortCols.Where(x => x.GetAttribute("fieldname") == columnName).FirstOrDefault();
                if (sortCol == null)
                    throw new InvalidOperationException($"Column: {columnName} Does not exist");
                else
                    sortCol.Click();
                return true;
            });
        }

        public BrowserCommandResult<List<XrmGridItem>> GetGridItems()
        {
            return this.Execute(GetOptions("Get Grid Items"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
                                && column.GetAttribute("class").Contains("ms-crm-List-DataColumn")
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

        public BrowserCommandResult<bool> OpenGridRow(int index)
        {
            return this.Execute(GetOptions("Open Grid Item"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var itemsTable = driver.WaitUntilAvailable(By.Id("gridBodyTable"));
                var links = itemsTable.FindElements(By.TagName("a"));

                var currentIndex = 0;
                var clicked = false;

                foreach (var link in links)
                {
                    var id = link.GetAttribute("id");

                    if (id != null && id.StartsWith("gridBodyTable_primaryField_"))
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
    }
}