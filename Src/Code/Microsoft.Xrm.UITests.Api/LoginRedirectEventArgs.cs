using OpenQA.Selenium;
using System;
using System.Security;

namespace Microsoft.Xrm.UITests.Api
{
    public class LoginRedirectEventArgs : EventArgs
    {
        protected internal LoginRedirectEventArgs(SecureString username, SecureString password, IWebDriver driver)
        {
            this.Username = username;
            this.Password = password;
            this.Driver = driver;
        }

        public SecureString Username { get; private set; }
        public SecureString Password { get; private set; }
        public IWebDriver Driver { get; private set; }
    }
}