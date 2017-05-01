﻿using OpenQA.Selenium;
using System;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmGlobalSearchPage
        : XrmPage
    {
        public XrmGlobalSearchPage(InteractiveBrowser browser)
            : base(browser)
        {
            this.SwitchToContentFrame();
        }

        /// <summary>
        /// Filter by entity in the Global Search Results.
        /// </summary>
        /// <param name="entity">The entity you want to filter with.</param>
        /// <param name="thinkTime">The think time.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> FilterWith(string entity, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Filter With: {entity}"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.GlobalSearch.Filter])))
                    throw new InvalidOperationException("Filter With picklist is not available");

                var picklist = driver.FindElement(By.XPath(Elements.Xpath[Reference.GlobalSearch.Filter]));
                var options = driver.FindElements(By.TagName("option"));
                
                picklist.Click();

                IWebElement select = options.FirstOrDefault(x => x.Text == entity);

                if (select == null)
                    throw new InvalidOperationException($"Entity {entity} does not exist in the Filter options.");

                select.Click();

                return true;
            });
        }
        /// <summary>
        /// Searches for the specified criteria in Global Search.
        /// </summary>
        /// <param name="criteria">Search criteria.</param>
        /// <param name="thinkTime">The think time.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Search(string criteria, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Global Search"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.GlobalSearch.SearchText])))
                    throw new InvalidOperationException("Search Text Box is not available");

                var searchText = driver.FindElement(By.XPath(Elements.Xpath[Reference.GlobalSearch.SearchText]));

                searchText.Click();
                searchText.SendKeys(criteria);

                driver.FindElement(By.XPath(Elements.Xpath[Reference.GlobalSearch.SearchButton])).Click();

                return true;
            });
        }

        /// <summary>
        /// Opens the specified record in the Global Search Results.
        /// </summary>
        /// <param name="entity">The entity you want to open a record.</param>
        /// <param name="index">The index of the record you want to open.</param>
        /// <param name="thinkTime">The think time.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> OpenRecord(string entity, int index, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Global Search Record"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.GlobalSearch.SearchResults])))
                    throw new InvalidOperationException("Search Results is not available");

                var results = driver.FindElement(By.XPath(Elements.Xpath[Reference.GlobalSearch.SearchResults]));
                var entityContainers = results.FindElements(By.ClassName(Elements.CssClass[Reference.GlobalSearch.EntityContainerClass]));
                var entityContainer = entityContainers.FirstOrDefault(x => x.Text.Trim() == entity);

                if(entityContainer==null)
                    throw new InvalidOperationException($"Entity {entity} was not found in the results");

                var records =  entityContainer?.FindElements(By.Id(Elements.ElementId[Reference.GlobalSearch.RecordNameId]));

                if (records == null)
                    throw new InvalidOperationException($"No records found for entity {entity}");

                records[index].Click();

                return true;
            });
        }
    }
}