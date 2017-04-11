using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmLookupPage
        : BrowserPage
    {
        public XrmLookupPage(InteractiveBrowser browser)
            : base(browser)
        {
            this.Browser.GetPage<XrmNavigationPage>().SwitchToDialogFrame();
        }

        internal BrowserCommandOptions GetOptions(string commandName)
        {
            return new BrowserCommandOptions(Constants.DefaultTraceSource,
                commandName,
                1,
                0,
                null,
                false,
                typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        private BrowserCommandResult<Dictionary<string, IWebElement>> OpenEntityPicker()
        {
            return this.Execute(GetOptions("Open Entity Picker"), driver =>
            {
                var dictionary = new Dictionary<string, IWebElement>();

                var entitySelectorContainer = driver.FindElement(By.Id("selObjects"));
                entitySelectorContainer.Click();

                Thread.Sleep(500);

                var entityItems = entitySelectorContainer.FindElements(By.TagName("option"));

                foreach (var entityItem in entityItems)
                {
                    var title = entityItem.GetAttribute("title");
                    dictionary.Add(title, entityItem);
                }

                return dictionary;
            });
        }
        private BrowserCommandResult<Dictionary<string, IWebElement>> OpenViewPicker()
        {
            return this.Execute(GetOptions("Open View Picker"), driver =>
            {
                var dictionary = new Dictionary<string, IWebElement>();

                var viewSelectorContainer = driver.WaitUntilAvailable(By.Id("crmGrid_SavedQuerySelector"));
                viewSelectorContainer.Click();

                Thread.Sleep(500);

                var viewItems = viewSelectorContainer.FindElements(By.TagName("option"));

                foreach (var viewItem in viewItems)
                {
                    var title = viewItem.GetAttribute("title");
                    dictionary.Add(title, viewItem);
                }

                return dictionary;
            });
        }

        public BrowserCommandResult<bool> SwitchEntity(string entityName)
        {
            return this.Execute(GetOptions("Switch Entity"), driver =>
            {
                var entities = OpenEntityPicker().Value;

                if (!entities.ContainsKey(entityName))
                    return false;

                var entityItem = entities[entityName];
                entityItem.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> SwitchView(string viewName)
        {
            return this.Execute(GetOptions("Switch View"), driver =>
            {
                var views = OpenViewPicker().Value;

                if (!views.ContainsKey(viewName))
                    return false;

                views[viewName].Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Search(string searchCriteria)
        {
            return this.Execute(GetOptions("Search"), driver =>
            {
                driver.FindElement(By.Id("crmGrid_findCriteria")).SendKeys(searchCriteria);
                driver.FindElement(By.Id("crmGrid_findCriteriaImg")).Click();

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

        public BrowserCommandResult<bool> SelectItem(int index)
        {
            return this.Execute(GetOptions("Select Item"), driver =>
            {
                var itemsTable = driver.WaitUntilAvailable(By.Id("gridBodyTable"));
                var items = itemsTable.FindElements(By.TagName("tr"));

                var item = items[index + 1];
                var checkbox = item.FindElements(By.TagName("td"))[0];

                checkbox.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectItem(string value)
        {
            return this.Execute(GetOptions("Select Item"), driver =>
            {
                var itemsTable = driver.WaitUntilAvailable(By.Id("gridBodyTable"));
                var tbody = itemsTable.FindElement(By.TagName("tbody"));
                var items = tbody.FindElements(By.TagName("tr"));

                foreach (var item in items)
                {
                    var primary = item.FindElements(By.TagName("td"))[1];
                    if (primary.Text == value)
                    {
                        var checkbox = item.FindElements(By.TagName("td"))[0];
                        checkbox.Click();
                        break;
                    }
                }

                return true;
            });
        }

        public BrowserCommandResult<bool> Add()
        {
            return this.Execute(GetOptions("Add"), driver =>
            {               
                var add = driver.FindElement(By.Id("butBegin"));

                add?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Select()
        {
            return this.Execute(GetOptions("Select"), driver =>
            {
                var add = driver.FindElement(By.Id("btnAdd"));

                add?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Remove()
        {
            return this.Execute(GetOptions("Remove"), driver =>
            {
                var add = driver.FindElement(By.Id("btnRemove"));

                add?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> New()
        {
            return this.Execute(GetOptions("New"), driver =>
            {
                var add = driver.FindElement(By.Id("btnNew"));

                add?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Cancel()
        {
            return this.Execute(GetOptions("Cancel"), driver =>
            {
                var add = driver.FindElement(By.Id("cmdDialogCancel"));

                add?.Click();

                return true;
            });
        }
    }
}