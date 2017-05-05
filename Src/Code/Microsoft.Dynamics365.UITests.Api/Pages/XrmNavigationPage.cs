using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;
using System.Threading;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmNavigationPage
        : XrmPage
    {
        private static string relatedId;

        public XrmNavigationPage(InteractiveBrowser browser)
            : base(browser)
        {
            SwitchToDefaultContent();
        }
        
        public BrowserCommandResult<bool> OpenHomePage(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            //TODO: Implement HomePage logic
            throw new NotImplementedException();
        }
        public BrowserCommandResult<Dictionary<string, IWebElement>> OpenHamburgerMenu(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Open Home Tab Menu"), driver => 
            {
                var dictionary = new Dictionary<string, IWebElement>();

                driver.ClickWhenAvailable(By.XPath(Elements.Xpath[Reference.Navigation.HomeTab]));

                Thread.Sleep(1000);

                var element = driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.ActionGroup]));
                var subItems = element.FindElements(By.ClassName(Elements.CssClass[Reference.Navigation.ActionGroupContainerClass]));

                foreach (var subItem in subItems)
                {
                    dictionary.Add(subItem.Text, subItem);
                }

                return dictionary;
            });
        }
        public BrowserCommandResult<bool> OpenSubArea(string area, string subArea, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($": {area} > {subArea}"), driver =>
            {
                var areas = OpenHamburgerMenu().Value;

                if (!areas.ContainsKey(area))
                {
                    throw new InvalidOperationException($"No area with the name '{area}' exists.");
                }

                var subAreas = OpenSubMenu(areas[area]).Value;

                if (!subAreas.ContainsKey(subArea))
                {
                    throw new InvalidOperationException($"No subarea with the name '{subArea}' exists inside of '{area}'.");
                }
                
                subAreas[subArea].Click();

                driver.WaitForPageToLoad();

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenRelated(string relatedArea, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Open Related Menu"), driver =>
            {
                driver.ClickWhenAvailable(By.XPath(Elements.Xpath[Reference.Navigation.TabNode]));

                var element = driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.ActionGroup]));
                var subItems = element.FindElements(By.ClassName("nav-rowBody"));

                var related = subItems.Where(x => x.Text == relatedArea).FirstOrDefault();
                relatedId = related.GetAttribute("id").Replace("Node_nav", "area");
                related?.Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> GlobalSearch(string searchText, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Global Search: {searchText}"), driver => 
            {
                //Narrow down the scope to the Search Tab when looking for the search input
                var navBar = driver.FindElement(By.Id("TabSearch"));
                var input = navBar.FindElement(By.Id("search"));

                input.SendKeys(searchText);

                navBar.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.FindCriteria])).Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenAdvancedFind(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Advanced Find"), driver=> 
            {
                //Narrow down the scope to the Search Tab when looking for the search input
                var navBar = driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.AdvFindSearch]));

                navBar.FindElement(By.ClassName("navTabButtonLink")).Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> QuickCreate(string entity,int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Quick Create"), driver=> 
            {
                driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.GlobalCreate]))?.Click();
                var area = driver.FindElement(By.ClassName("navActionGroupContainer"));
                var items = area.FindElements(By.ClassName("nav-rowLabel"));
                var item = items.FirstOrDefault(x => x.Text == entity);

                item?.Click();
                return true;
            });
        }
        public BrowserCommandResult<bool> SignOut(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"SignOut"), driver => 
            {
                var userInfo = driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.UserInfo]));
                userInfo?.Click();
                var signOut = driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.SignOut]));
                signOut?.Click();
                return true;
            });
        }
        public BrowserCommandResult<List<XrmLink>> OpenMruMenu(int thinkTime = Constants.DefaultThinkTime)
        {
            return this.Execute(GetOptions("Open MRU Menu"), driver =>
            {
                var list = new List<XrmLink>();

                var mruSpan = driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.TabGlobalMruNode]));
                mruSpan.Click();

                var navContainer = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Navigation.Shuffle]));
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
        public BrowserCommandResult<bool> OpenGuidedHelp(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Guided Help"), driver =>
            {
                driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.GuidedHelp]))?.Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenAdminPortal(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Admin Portal"), driver =>
            {
                driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.AdminPortal]))?.Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenOptions(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Options"), driver =>
            {
                OpenSettingsOption(driver, Elements.Xpath[Reference.Navigation.Options]);

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenPrintPreview(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open PrintPreview"), driver =>
            {
                OpenSettingsOption(driver, Elements.Xpath[Reference.Navigation.PrintPreview]);

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenAppsForDynamicsCRM(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Apps for Dynamics"), driver =>
            {
                OpenSettingsOption(driver, Elements.Xpath[Reference.Navigation.AppsForCRM]);

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenWelcomeScreen(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Welcome Screen"), driver =>
            {
                OpenSettingsOption(driver, Elements.Xpath[Reference.Navigation.WelcomeScreen]);

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenAbout(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open About"), driver =>
            {
                OpenSettingsOption(driver, Elements.Xpath[Reference.Navigation.About]);

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenOptOutLearningPath(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Opt out of Learning Path"), driver =>
            {
                OpenSettingsOption(driver, Elements.Xpath[Reference.Navigation.OptOutLP]);

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenPrivacyStatement(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Privacy Statement"), driver =>
            {
                OpenSettingsOption(driver, Elements.Xpath[Reference.Navigation.Privacy]);

                return true;
            });
        }


        internal BrowserCommandResult<Dictionary<string, IWebElement>> OpenSubMenu(IWebElement area)
        {
            return this.Execute(GetOptions($"Open Sub Menu: {area}"), driver =>
            {
                var dictionary = new Dictionary<string, IWebElement>();

                driver.WaitUntilVisible(By.Id(area.GetAttribute("Id")));

                area.Click();

                Thread.Sleep(1000);

                driver.WaitUntilVisible(By.XPath(Elements.Xpath[Reference.Navigation.SubActionGroupContainer]));

                var subNavElement = driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.SubActionGroupContainer]));

                var subItems = subNavElement.FindElements(By.ClassName(Elements.CssClass[Reference.Navigation.SubActionElementClass]));

                foreach (var subItem in subItems)
                {
                    dictionary.Add(subItem.Text, subItem);
                }

                return dictionary;
            });
        }

        private static void OpenSettingsOption(IWebDriver driver, string settingPath)
        {
            driver.FindElement(By.XPath(Elements.Xpath[Reference.Navigation.Settings]))?.Click();
            Thread.Sleep(1000);
            driver.ClickWhenAvailable(By.XPath(settingPath));
        }
    }
}