using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmEntityPage
        : XrmPage 
    {        
        public XrmEntityPage(InteractiveBrowser browser)
            : base(browser)
        {
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

        private readonly string _navigateDownCssSelector = "img.recnav-down.ms-crm-ImageStrip-Down_Enabled_proxy";
        private readonly string _navigateUpCssSelector = "img.recnav-up.ms-crm-ImageStrip-Up_Enabled_proxy";

        public BrowserCommandResult<bool> OpenEntity(string entityName, Guid id)
        {
            return this.Execute(GetOptions($"Open: {entityName} {id}"), driver =>
            {
                var uri = new Uri(this.Browser.Driver.Url);
                var link = $"{uri.Scheme}://{uri.Authority}/main.aspx?etn={entityName}&pagetype=entityrecord&id=%7B{id:D}%7D";
                return OpenEntity(new Uri(link)).Value;
            });
        }

        public BrowserCommandResult<bool> OpenEntity(Uri uri)
        {
            return this.Execute($"Open: {uri}", driver =>
            {
                driver.Navigate().GoToUrl(uri);

                driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));

                return true;
            });
        }

        public BrowserCommandResult<bool> NavigateDown()
        {
            //return this.Execute("Navigate Down", NavigateUpDown, _navigateDownCssSelector);
            return this.Execute(GetOptions("Navigate Down"), driver =>
            {
                if (!driver.HasElement(By.CssSelector(_navigateDownCssSelector)))
                    return false;

                var buttons = driver.FindElements(By.CssSelector(_navigateDownCssSelector));

                buttons[0].Click();

                driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));

                return true;
            });
        }

        public BrowserCommandResult<bool> NavigateUp()
        {
            //return this.Execute("Navigate Down", NavigateUpDown, _navigateUpCssSelector);
            return this.Execute(GetOptions("Navigate Up"), driver =>
            {
                if (!driver.HasElement(By.CssSelector(_navigateUpCssSelector)))
                    return false;

                var buttons = driver.FindElements(By.CssSelector(_navigateUpCssSelector));

                buttons[0].Click();

                driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectForm(string name)
        {
            return this.Execute(GetOptions($"SelectForm: {name}"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id("formselectorcontainer"))?.FindElement(By.TagName("a"))?.Click();
                var items = driver.FindElements(By.ClassName("ms-crm-FS-MenuItem-Title"));
                items.Where(x => x.Text == name).FirstOrDefault()?.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> SelectTab(string name)
        {
            return this.Execute(GetOptions($"SelectTab: {name}"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var sections = driver.FindElements(By.ClassName("ms-crm-InlineTabHeaderText"));
                sections.Where(x => x.FindElement(By.TagName("h2")).Text == name).FirstOrDefault()?.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, [Range(0, 9)]int index)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver => 
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName("Lookup_RenderButton_td")) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_{field}_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (dialogItems.Count < index)
                        throw new InvalidOperationException($"List does not have {index + 1} items.");

                    var dialogItem = dialogItems.Values.ToList()[index];
                    dialogItem.Click();
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, string value)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver=> 
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName("Lookup_RenderButton_td")) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    var lookupIcon = input.FindElement(By.ClassName("Lookup_RenderButton_td"));
                    lookupIcon.Click();

                    var dialogName = $"Dialog_{field}_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (!dialogItems.Keys.Contains(value))
                        throw new InvalidOperationException($"List does not have {value}.");

                    var dialogItem = dialogItems[value];
                    dialogItem.Click();
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, bool openLookupPage = true)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver=> 
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName("Lookup_RenderButton_td")) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_{field}_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    var dialogItem = dialogItems.Values.Last();

                    dialogItem?.Click();
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }
  
        private BrowserCommandResult<Dictionary<string, IWebElement>> OpenDialog(IWebElement dialog)
        {
            var dictionary = new Dictionary<string, IWebElement>();
            var dialogItems = dialog.FindElements(By.TagName("li"));

            foreach (var dialogItem in dialogItems)
            {
                if (dialogItem.GetAttribute("role") != null && dialogItem.GetAttribute("role") == "menuitem")
                {
                    var links = dialogItem.FindElements(By.TagName("a"));

                    if (links != null && links.Count > 1)
                    {
                        var title = links[1].GetAttribute("title");

                        dictionary.Add(title, links[1]);
                    }
                }
            }

            return dictionary;
        }

        public BrowserCommandResult<bool> Popout()
        {
            return this.Execute(GetOptions($"Popout"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.ClassName("ms-crm-ImageStrip-popout"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> ClickSubgridAddButton(string subgridName)
        {
            return this.Execute(GetOptions($"Click add button of {subgridName}"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id($"{subgridName}_addImageButton"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> ClickSubgridGridViewButton(string subgridName)
        {
            return this.Execute(GetOptions($"Click GridView button of {subgridName}"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id($"{subgridName}_openAssociatedGridViewImageButtonImage"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectSubgridLookup(string subgridName, string value)
        {
            return this.Execute(GetOptions($"Set Lookup Value for Subgrid {subgridName}"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();
                    
                    var lookupIcon = input.FindElement(By.ClassName("Lookup_RenderButton_td"));
                    lookupIcon.Click();

                    var dialogName = $"Dialog_lookup_{subgridName}_i_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (!dialogItems.Keys.Contains(value))
                        throw new InvalidOperationException($"List does not have {value}.");

                    var dialogItem = dialogItems[value];
                    dialogItem.Click();
                }

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectSubgridLookup(string subgridName, [Range(0, 9)]int index)
        {
            return this.Execute(GetOptions($"Set Lookup Value for Subgrid {subgridName}"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();

                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_lookup_{subgridName}_i_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (dialogItems.Count < index)
                        throw new InvalidOperationException($"List does not have {index + 1} items.");

                    var dialogItem = dialogItems.Values.ToList()[index];
                    dialogItem.Click();
                }

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectSubgridLookup(string subgridName, bool openLookupPage = true)
        {
            return this.Execute(GetOptions($"Set Lookup Value for Subgrid {subgridName}"), driver =>
            {
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();
                    
                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_lookup_{subgridName}_i_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    var dialogItem = dialogItems.Values.Last();

                    dialogItem?.Click();
                }

                return true;
            });
        }
    }
}