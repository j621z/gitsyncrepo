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
            SwitchToContentFrame();
        }

        private readonly string _navigateDownCssSelector = "img.recnav-down.ms-crm-ImageStrip-Down_Enabled_proxy";
        private readonly string _navigateUpCssSelector = "img.recnav-up.ms-crm-ImageStrip-Up_Enabled_proxy";

        public BrowserCommandResult<bool> OpenEntity(string entityName, Guid id, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open: {entityName} {id}"), driver =>
            {
                var uri = new Uri(this.Browser.Driver.Url);
                var link = $"{uri.Scheme}://{uri.Authority}/main.aspx?etn={entityName}&pagetype=entityrecord&id=%7B{id:D}%7D";
                return OpenEntity(new Uri(link)).Value;
            });
        }

        public BrowserCommandResult<bool> OpenEntity(Uri uri, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open: {uri}"), driver =>
            {

                driver.Navigate().GoToUrl(uri);

                DismissAlertIfPresent();

                // driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));
                driver.WaitForPageToLoad();

                return true;
            });
        }

        public BrowserCommandResult<bool> NavigateDown(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Navigate Down"), driver =>
            {
                SwitchToDefaultContent();
                if (!driver.HasElement(By.CssSelector(_navigateDownCssSelector)))
                    return false;

                var buttons = driver.FindElements(By.CssSelector(_navigateDownCssSelector));

                buttons[0].Click();

                //driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));
                driver.WaitForPageToLoad();

                return true;
            });
        }

        public BrowserCommandResult<bool> NavigateUp(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Navigate Up"), driver =>
            {
                SwitchToDefaultContent();
                if (!driver.HasElement(By.CssSelector(_navigateUpCssSelector)))
                    return false;

                var buttons = driver.FindElements(By.CssSelector(_navigateUpCssSelector));

                buttons[0].Click();

                //driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));
                driver.WaitForPageToLoad();
                
                return true;
            });
        }

        public BrowserCommandResult<bool> SelectForm(string name, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"SelectForm: {name}"), driver =>
            { 
                var form = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Entity.SelectForm]) ,"The Select Form option is not available.");
                form.Click();

                var items = driver.FindElement(By.XPath(Elements.Xpath[Reference.Entity.ContentTable])).FindElements(By.TagName("a"));
                items.Where(x => x.Text == name).FirstOrDefault()?.Click();

                return true;

            });
        }

        /// <summary>
        /// Selects the tab and clicks. If the tab is expanded it will collapse it. If the tab is collapsed it will expand it. 
        /// </summary>
        /// <param name="name">The name of the tab.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SelectTab(string name, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"SelectTab: {name}"), driver =>
            {
                var section = driver.FindElement(By.Id(Elements.ElementId[Reference.Entity.Tab].Replace("[NAME]", name.ToUpper())));

                if (section == null)
                    throw new InvalidOperationException($"Section with name {name} does not exist.");
                
                section?.Click();

                return true;
            });
        }
        /// <summary>
        /// Collapses the Tab on a CRM Entity form.
        /// </summary>
        /// <param name="name">The name of the Tab.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> CollapseTab(string name, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Collapse Tab: {name}"), driver =>
            {
                var section = driver.FindElement(By.Id(Elements.ElementId[Reference.Entity.Tab].Replace("[NAME]",name.ToUpper())));

                if (section == null)
                    throw new InvalidOperationException($"Section with name {name} does not exist.");

               if (section.FindElement(By.TagName("img")).GetAttribute("title").Contains("Collapse"))
                    section?.Click();

                return true;
            });
        }
        /// <summary>
        /// Expands the Tab on a CRM Entity form.
        /// </summary>
        /// <param name="name">The name of the Tab.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> ExpandTab(string name, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Expand Tab: {name}"), driver =>
            {
                var section = driver.FindElement(By.Id(Elements.ElementId[Reference.Entity.Tab].Replace("[NAME]", name.ToUpper())));

                if (section == null)
                    throw new InvalidOperationException($"Section with name {name} does not exist.");

                if (section.FindElement(By.TagName("img")).GetAttribute("title").Contains("Expand"))
                    section?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, [Range(0, 9)]int index)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver => 
            {
                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.LookupRenderClass])) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    input.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.LookupRenderClass])).Click();

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
                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.LookupRenderClass])) == null)
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
                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.LookupRenderClass])) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    input.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.LookupRenderClass])).Click();

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

        public BrowserCommandResult<bool> Popout(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Popout"), driver =>
            {
                SwitchToDefaultContent();
                driver.FindElement(By.ClassName(Elements.CssClass[Reference.Entity.Popout]))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> ClickSubgridAddButton(string subgridName, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Click add button of {subgridName}"), driver =>
            {
                driver.FindElement(By.Id($"{subgridName}_addImageButton"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> ClickSubgridGridViewButton(string subgridName, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Click GridView button of {subgridName}"), driver =>
            {
                driver.FindElement(By.Id($"{subgridName}_openAssociatedGridViewImageButtonImage"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectSubgridLookup(string subgridName, string value, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Set Lookup Value for Subgrid {subgridName}"), driver =>
            {
                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();
                    
                    var lookupIcon = input.FindElement(By.ClassName(Elements.CssClass[Reference.Entity.LookupRender]));
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
                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();

                    input.FindElement(By.ClassName(Elements.CssClass[Reference.Entity.LookupRender])).Click();

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
                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();
                    
                    input.FindElement(By.ClassName(Elements.CssClass[Reference.Entity.LookupRender])).Click();

                    var dialogName = $"Dialog_lookup_{subgridName}_i_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    var dialogItem = dialogItems.Values.Last();

                    dialogItem?.Click();
                }

                return true;
            });
        }

        /// <summary>
        /// Closes the current entity record you are on.
        /// </summary>
        /// <returns></returns>
        public BrowserCommandResult<bool> CloseEntity(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Close Entity"), driver =>
            {
                SwitchToDefaultContent();

                var filter = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Entity.Close]),
                    "Close Buttton is not available");

                filter?.Click();

                return true;
            });
        }
        /// <summary>
        /// Saves the specified entity record.
        /// </summary>
        /// <param name="thinkTime">The think time.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Save(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions("Save Entity"), driver =>
            {
                var save = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Entity.Save]),
                    "Save Buttton is not available");

                save?.Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> DismissAlertIfPresent(bool stay = false)
        {

            return this.Execute(GetOptions("Dismiss Confirm Save Alert"), driver =>
            {
                if (driver.AlertIsPresent())
                {
                    if (stay)
                        driver.SwitchTo().Alert().Dismiss();
                    else
                        driver.SwitchTo().Alert().Accept();
                }

                return true;
            });
        }
    }
}