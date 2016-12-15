using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Xrm.UITests.Api
{
    public class XrmNavigationPage
        : BrowserPage
    {
        private static string relatedId;

        public XrmNavigationPage(InteractiveBrowser browser)
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

        public BrowserCommandResult<Dictionary<string, Uri>> OpenHamburgerMenu()
        {
            return this.Execute(GetOptions("Open Hamburger Menu"), driver => 
            {
                var dictionary = new Dictionary<string, Uri>();

                driver.ClickWhenAvailable(By.Id("HomeTabLink"));

                var element = driver.FindElement(By.Id("actionGroupControl"));
                var subItems = element.FindElements(By.ClassName("navActionButtonContainer"));

                foreach (var subItem in subItems)
                {
                    dictionary.Add(subItem.Text, new Uri(subItem.GetAttribute("href")));
                }

                return dictionary;
            });
        }

        public BrowserCommandResult<Dictionary<string, Uri>> OpenSubMenu(string area)
        {
            return this.Execute(GetOptions($"Open Sub Menu: {area}"), driver=> 
            {
                var dictionary = new Dictionary<string, Uri>();

                var element = driver.FindElement(By.Id("actionGroupControl"));
                var menuItem = element.FindElement(By.LinkText(area));
                menuItem.Click();

                driver.WaitUntilVisible(By.Id("detailActionGroupControl"));

                var subNavElement = driver.FindElement(By.Id("detailActionGroupControl"));
                //element.Click();

                var subItems = subNavElement.FindElements(By.ClassName("nav-rowBody"));

                foreach (var subItem in subItems)
                {
                    dictionary.Add(subItem.Text, new Uri(subItem.GetAttribute("href")));
                }

                return dictionary;
            });
        }

        public BrowserCommandResult<bool> OpenSubArea(string area, string subArea)
        {
            return this.Execute(GetOptions($": {area} > {subArea}"), driver =>
            {
                var areas = OpenHamburgerMenu().Value;

                if (!areas.ContainsKey(area))
                {
                    throw new InvalidOperationException($"No area with the name '{area}' exists.");
                }

                var subAreas = OpenSubMenu(area).Value;

                if (!subAreas.ContainsKey(subArea))
                {
                    throw new InvalidOperationException($"No subarea with the name '{subArea}' exists inside of '{area}'.");
                }

                var url = subAreas[subArea];

                driver.Navigate().GoToUrl(url);
                driver.WaitForPageToLoad();

                return true;
            });
        }

        public BrowserCommandResult<bool> OpenRelated(string relatedArea)
        {
            return this.Execute(GetOptions("Open Related Menu"), driver =>
            {
                driver.SwitchTo().DefaultContent();

                driver.ClickWhenAvailable(By.Id("TabNode_tab0Tab"));

                var element = driver.FindElement(By.Id("actionGroupControl"));
                var subItems = element.FindElements(By.ClassName("nav-rowBody"));

                var related = subItems.Where(x => x.Text == relatedArea).FirstOrDefault();
                relatedId = related.GetAttribute("id").Replace("Node_nav", "area");
                related?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> GlobalSearch(string searchText)
        {
            return this.Execute(GetOptions($"Global Search: {searchText}"), driver => 
            {
                //Narrow down the scope to the Search Tab when looking for the search input
                var navBar = driver.FindElement(By.Id("TabSearch"));
                var input = navBar.FindElement(By.Id("search"));

                input.SendKeys(searchText);

                navBar.FindElement(By.Id("findCriteriaButton")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> OpenAdvancedFind()
        {
            return this.Execute(GetOptions($"Open Advanced Find"), driver=> 
            {
                //Narrow down the scope to the Search Tab when looking for the search input
                var navBar = driver.FindElement(By.Id("AdvFindSearch"));

                navBar.FindElement(By.ClassName("navTabButtonLink")).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> QuickCreate(string entity)
        {
            return this.Execute(GetOptions($"Open Quick Create"), driver=> 
            {
                driver.FindElement(By.Id("navTabGlobalCreateImage"))?.Click();
                var area = driver.FindElement(By.ClassName("navActionGroupContainer"));
                var items = area.FindElements(By.ClassName("nav-rowLabel"));
                var item = items.Where(x => x.Text == entity).FirstOrDefault();

                item?.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> SignOut()
        {
            return this.Execute(GetOptions($"SignOut"), driver => 
            {
                var userInfo = driver.FindElement(By.Id("navTabButtonUserInfoLinkId"));
                userInfo?.Click();
                var signOut = driver.FindElement(By.Id("navTabButtonUserInfoSignOutId"));
                signOut?.Click();
                return true;
            });
        }

        public BrowserCommandResult<List<XrmLink>> OpenMruMenu()
        {
            return this.Execute(GetOptions("Open MRU Menu"), driver =>
            {
                var list = new List<XrmLink>();

                var mruSpan = driver.FindElement(By.Id("TabGlobalMruNode"));
                mruSpan.Click();

                var navContainer = driver.WaitUntilAvailable(By.Id("nav-shuffle"));
                var links = navContainer.FindElements(By.TagName("a"));

                foreach (var link in links)
                {
                    if (!string.IsNullOrEmpty(link.Text))
                    {
                        var newItem = new XrmLink();

                        newItem.Uri = new Uri(link.GetAttribute("href"));
                        newItem.PageType = newItem.Uri.ToPageType();
                        newItem.LinkText = link.Text;

                        list.Add(newItem);
                    }
                }

                return list;
            });
        }
        
        
        public bool SwitchToContentFrame()
        {
            return this.Execute("Switch to content frame", driver =>
            {
                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.Id("crmContentPanel"));

                //find the crmContentPanel and find out what the current content frame ID is - then navigate to the current content frame
                var currentContentFrame = driver.FindElement(By.Id("crmContentPanel")).GetAttribute("currentcontentid");

                driver.SwitchTo().Frame(currentContentFrame);


                return true;
            });
        }

        public bool SwitchToDialogFrame()
        {
            return this.Execute("Switch to dialog frame", driver =>
            {
                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.Id("InlineDialog"));

                driver.SwitchTo().Frame("InlineDialog_Iframe");

                return true;
            });
        }

        public bool SwitchToQuickFindFrame()
        {
            return this.Execute("Switch to QuickFind Frame", driver =>
            {
                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.Id("globalquickcreate_container_NavBarGloablQuickCreate"));

                driver.SwitchTo().Frame("NavBarGloablQuickCreate");

                return true;
            });
        }

        public bool SwitchToRelatedFrame()
        {
            return this.Execute("Switch to Related Frame", driver =>
            {
                SwitchToContentFrame();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.Id(relatedId));

                driver.SwitchTo().Frame(relatedId + "Frame");

                return true;
            });
        }

    }
}