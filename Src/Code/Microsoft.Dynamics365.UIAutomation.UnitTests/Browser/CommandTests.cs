using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void TestDelegateCommand()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                var command = new DelegateBrowserCommand<string>(
                    new BrowserCommandOptions(Constants.DefaultTraceSource,
                        string.Empty,
                        Constants.DefaultRetryAttempts,
                        Constants.DefaultRetryDelay,
                        null,
                        true,
                        typeof(InvalidOperationException)),
                        d =>
                        {
                            throw new InvalidOperationException("test");
                        });

                var result = command.Execute(xrmBrowser.Driver);

                Assert.IsNotNull(result.Success);
                Assert.IsFalse(result.Success.Value);
				Assert.AreEqual(Constants.DefaultRetryAttempts, result.ExecutionAttempts);
				Assert.IsTrue(result.ExecutionTime > TimeSpan.FromMilliseconds(Constants.DefaultRetryDelay * Constants.DefaultRetryAttempts).Milliseconds);
				Assert.AreEqual(String.Empty, result.Value);
            }
        }
    }
}