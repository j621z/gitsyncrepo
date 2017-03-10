using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;
using Microsoft.Dynamics365.UITests.Translation;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class TranslationTest
    {
        [TestMethod]
        public void TranslateElementIdTest()
        {
            var elementId = "HomeTabLink";

            EventRepository repos = new EventRepository(new CodeProvider());

            Event ev = repos.Search(elementId);

            string apiCall = ev.ApiCall;

        }
    }
}