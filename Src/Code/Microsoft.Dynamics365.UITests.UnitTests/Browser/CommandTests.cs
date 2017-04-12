using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void TestDelegateCommand()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true
            }))
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

                Assert.IsNotNull(result.Result.Success);
                Assert.IsFalse(result.Result.Success.Value);
				Assert.AreEqual(Constants.DefaultRetryAttempts, result.Result.ExecutionAttempts);
				Assert.IsTrue(result.Result.ExecutionTime > TimeSpan.FromMilliseconds(Constants.DefaultRetryDelay * Constants.DefaultRetryAttempts));
				Assert.AreEqual(String.Empty, result.Value);
            }
        }
    }
}