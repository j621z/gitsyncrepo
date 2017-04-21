using OpenQA.Selenium;
using System;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api.Pages
{
    public class XrmRecordWallPage: XrmPage
    {
        public XrmRecordWallPage(InteractiveBrowser browser)
            : base(browser)
        {
            this.SwitchToContentFrame();
        }

        public enum Tab
        {
            Posts,
            Activities,
            Notes
        }

        /// <summary>
        /// Selects the tab.
        /// </summary>
        /// <param name="tabname">The tabname.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SelectTab(Tab tabname, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Select Tab on the Record Wall"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.RecordWall.NotesControl])))
                    throw new InvalidOperationException("The Record Wall is not available.");

                var notesControl = driver.FindElement(By.XPath(Elements.Xpath[Reference.RecordWall.NotesControl]));
                var tabs = notesControl.FindElements(By.TagName("a"));

                var tab = tabs.FirstOrDefault(x => x.Text == tabname.ToString().ToUpper());

                if (tab == null)
                    throw new InvalidOperationException("The Tab is not available.");

                tab.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> AddNote(string noteText, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Add Note"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.RecordWall.NotesWall])))
                    throw new InvalidOperationException("The Notes Wall is not available. Please check that the Notes tab is selected.");

                var wall = driver.FindElement(By.XPath(Elements.Xpath[Reference.RecordWall.NotesWall]));
                var text = wall.FindElement(By.XPath(Elements.Xpath[Reference.RecordWall.NotesText]));

                text.Click();
                text.SendKeys(noteText);

                wall.FindElement(By.XPath(Elements.Xpath[Reference.RecordWall.NotesButton])).Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> AddPost(string postText, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Add Post"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.RecordWall.NotesWall])))
                    throw new InvalidOperationException("The Post Wall is not available. Please check that the Posts tab is selected.");

                var wall = driver.FindElement(By.XPath(Elements.Xpath[Reference.RecordWall.PostWall]));
                var text = wall.FindElement(By.XPath(Elements.Xpath[Reference.RecordWall.PostText]));

                text.Click();
                text.SendKeys(postText);

                wall.FindElement(By.XPath(Elements.Xpath[Reference.RecordWall.PostButton])).Click();

                return true;
            });
        }
    }
}
