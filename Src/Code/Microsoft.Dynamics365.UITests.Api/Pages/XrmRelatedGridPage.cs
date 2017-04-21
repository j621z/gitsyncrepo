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

        public BrowserCommandResult<Dictionary<string, Guid>> OpenViewPicker()
        {
            return this.Execute("Open View Picker", driver =>
            {
                var dictionary = new Dictionary<string, Guid>();

                var viewSelectorContainer = driver.WaitUntilAvailable(By.Id("viewSelectorContainer"));
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
                driver.FindElement(By.Id("grid_refresh")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> FirstPage()
        {
            return this.Execute(GetOptions("FirstPage"), driver =>
            {
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
                var previousIcon = driver.FindElement(By.Id("_prevPageImg"));

                if (previousIcon.GetAttribute("disabled") != null)
                    return false;
                else
                    previousIcon.Click();
                return true;
            });
        }
       
        public BrowserCommandResult<bool> Search(string searchCriteria)
        {
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

        public BrowserCommandResult<bool> Sort(string columnName)
        {
            return this.Execute(GetOptions($"Sort by {columnName}"), driver =>
            {                
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

        private BrowserCommandResult<ReadOnlyCollection<IWebElement>> GetCommands(bool moreCommands = false)
        {
            return this.Execute(GetOptions("Get Command Bar Buttons"), driver =>
            {
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
            return this.Execute(GetOptions("ClickCommand"), driver =>
            {
                if (moreCommands)
                    driver.FindElement(By.Id("moreCommands")).Click();

                var buttons = GetCommands(moreCommands).Value;
                var button = buttons.Where(x => x.Text.ToLower() == name.ToLower()).FirstOrDefault();

                if (string.IsNullOrEmpty(subName))
                    button.Click();
                else
                {
                    button.FindElement(By.ClassName("ms-crm-Menu-Label-Flyout")).Click();

                    var flyoutId = button.GetAttribute("id").Replace("|", "_").Replace(".", "_") + "Menu";
                    var subButtons = driver.FindElement(By.Id(flyoutId)).FindElements(By.TagName("li"));
                    subButtons.Where(x => x.Text.ToLower() == subName.ToLower()).FirstOrDefault()?.Click();
                }

                return true;
            });
        }
    }
}