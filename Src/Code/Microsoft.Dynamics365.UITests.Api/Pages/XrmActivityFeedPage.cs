using OpenQA.Selenium;
using System;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api.Pages
{
    public class XrmActivityFeedPage: XrmPage
    {
        public XrmActivityFeedPage(InteractiveBrowser browser)
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
        public enum Status
        {
            All,
            InProgress,
            Overdue
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
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.NotesControl])))
                    throw new InvalidOperationException("The Record Wall is not available.");

                var notesControl = driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.NotesControl]));
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
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.NotesWall])))
                    throw new InvalidOperationException("The Notes Wall is not available. Please check that the Notes tab is selected.");

                var wall = driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.NotesWall]));
                var text = wall.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.NotesText]));
                text.Click();
                var textArea = wall.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.NotesText]));
                textArea.Click();
                textArea.SendKeys(noteText);

               var post= driver.FindElement(By.Id("doneSpacer"));
                var done=post.FindElement(By.Id("postButton"));
                done.Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> AddPost(string postText, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Add Post"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.PostWall])))
                    throw new InvalidOperationException("The Post Wall is not available. Please check that the Posts tab is selected.");

                var wall = driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.PostWall]));
                var text = wall.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.PostText]));

                text.Click();
                text.SendKeys(postText);

                wall.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.PostButton])).Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> FilterActivitiesByStatus(Status status, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Filter Activity by Status"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityWall])))
                    throw new InvalidOperationException("The Activity Wall is not available. Please check that the Activities tab is selected.");

                var wall = driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityWall]));
                wall.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityStatusFilter])).Click();
                var dialog = driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityStatusFilterDialog]));

                switch(status)
                {
                    case Status.All:
                        dialog.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityStatusAll])).Click();
                        break;
                    case Status.InProgress:
                        dialog.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityStatusOpen])).Click();
                        break;
                    case Status.Overdue:
                        dialog.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivitySTatusOverdue])).Click();
                        break;
                }

                return true;
            });
        }
        public BrowserCommandResult<bool> OpenActivitiesAssociatedView(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Open Activities Associated View"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityWall])))
                    throw new InvalidOperationException("The Activity Wall is not available. Please check that the Activities tab is selected.");

                driver.ClickWhenAvailable(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityAssociatedView]));
                
                return true;
            });
        }

        public BrowserCommandResult<bool> AddPhoneCall(string description, bool leftVoiceMail, bool outgoing = true, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Add Phone Call from Activity Feed"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityWall])))
                    throw new InvalidOperationException("The Activity Feed is not available. Please check that the Activities tab is selected.");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityAddPhoneCall])).Click();

                this.SetValue(Elements.ElementId[Reference.ActivityFeed.ActivityPhoneCallDescriptionId], description);

                var mailId=driver.FindElement(By.Id("PhoneCallQuickformleftvoiceCheckBoxContol"));
                mailId.Click();

                if (!outgoing)
                    driver.FindElement(By.Id(Elements.ElementId[Reference.ActivityFeed.ActivityPhoneCallDirectionId])).Click();

                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityPhoneCallOk])).Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> AddTask(string subject, string description, DateTime dueDate, OptionSet priority, int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Add Task from Activity Feed"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityWall])))
                    throw new InvalidOperationException("The Activity Feed is not available. Please check that the Activities tab is selected.");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityAddTask])).Click();

                this.SetValue(Elements.ElementId[Reference.ActivityFeed.ActivityTaskSubjectId], subject);
                this.SetValue(Elements.ElementId[Reference.ActivityFeed.ActivityTaskDescriptionId], description);
                this.SetValue(Elements.ElementId[Reference.ActivityFeed.ActivityTaskScheduledEndId], dueDate);
                this.SetValue(priority);

                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityTaskOk])).Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> AddEmail(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Add Email from Activity Feed"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityWall])))
                    throw new InvalidOperationException("The Activity Feed is not available. Please check that the Activities tab is selected.");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityMoreActivities])).Click();
                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityAddTask])).Click();

                return true;
            });
        }
        public BrowserCommandResult<bool> AddAppointment(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute(GetOptions($"Add Email from Activity Feed"), driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityWall])))
                    throw new InvalidOperationException("The Activity Feed is not available. Please check that the Activities tab is selected.");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityMoreActivities])).Click();
                driver.FindElement(By.XPath(Elements.Xpath[Reference.ActivityFeed.ActivityAddAppointment])).Click();

                return true;
            });
        }
    }
}
