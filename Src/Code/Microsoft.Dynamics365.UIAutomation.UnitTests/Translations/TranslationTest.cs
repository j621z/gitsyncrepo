using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;
using Microsoft.Dynamics365.UIAutomation.Translation;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests
{
    [TestClass]
    public class TranslationTest
    {
        [TestMethod]
        public void TestTranslateElementId()
        {
            var elementId = "HomeTabLink";

            EventRepository repos = new EventRepository(new CodeProvider());

            Event ev = repos.Search(elementId);

            string apiCall = ev.ApiCall;

        }
    }
}