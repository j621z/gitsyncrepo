using OpenQA.Selenium;
using System;
using System.Linq;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    public enum LoginResult
    {
        Success,
        Failure,
        Redirect
    }

    public class LoginPage
        : XrmPage
    {
        public string[] OnlineDomains { get; set; }

        public LoginPage(InteractiveBrowser browser)
            : base(browser)
        {
            this.OnlineDomains = Constants.Xrm.XrmDomains;
        }

        public LoginPage(InteractiveBrowser browser, params string[] onlineDomains)
            : this(browser)
        {
            this.OnlineDomains = onlineDomains;
        }

        public BrowserCommandResult<LoginResult> Login()
        {
            return this.Login(new Uri(Constants.DefaultLoginUri));
        }

        public BrowserCommandResult<LoginResult> Login(SecureString username, SecureString password)
        {
            return this.Execute("Login", this.Login, new Uri(Constants.DefaultLoginUri), username, password, default(Action<LoginRedirectEventArgs>));
        }

        public BrowserCommandResult<LoginResult> Login(SecureString username, SecureString password, Action<LoginRedirectEventArgs> redirectAction)
        {
            return this.Execute("Login", this.Login, new Uri(Constants.DefaultLoginUri), username, password, redirectAction);
        }

        public BrowserCommandResult<LoginResult> Login(Uri uri)
        {
            if (this.Browser.Options.Credentials.IsDefault)
                throw new InvalidOperationException("The default login method cannot be invoked without first setting credentials on the Browser object.");

            return this.Execute("Login", this.Login, uri, this.Browser.Options.Credentials.Username, this.Browser.Options.Credentials.Password, default(Action<LoginRedirectEventArgs>));
        }

        public BrowserCommandResult<LoginResult> Login(Uri uri, SecureString username, SecureString password)
        {
            return this.Execute("Login", this.Login, uri, username, password, default(Action<LoginRedirectEventArgs>));
        }

        public BrowserCommandResult<LoginResult> Login(Uri uri, SecureString username, SecureString password, Action<LoginRedirectEventArgs> redirectAction)
        {
            return this.Execute("Login", this.Login, uri, username, password, redirectAction);
        }

        private LoginResult Login(IWebDriver driver, Uri uri, SecureString username, SecureString password, Action<LoginRedirectEventArgs> redirectAction)
        {
            var redirect = false;
            bool online = !(this.OnlineDomains != null && !this.OnlineDomains.Any(d => uri.Host.EndsWith(d)));
            
            driver.Navigate().GoToUrl(uri);

            if (online)
            {
                driver.WaitUntilAvailable(By.Id("cred_userid_inputtext"),
                    $"The Office 365 sign in page did not return the expected result and the user '{username}' could not be signed in.");

                driver.FindElement(By.Id("cred_userid_inputtext")).SendKeys(username.ToUnsecureString());
                driver.FindElement(By.Id("cred_userid_inputtext")).SendKeys(Keys.Enter);

                //If expecting redirect then wait for redirect to trigger
                if(redirectAction!= null) Thread.Sleep(3000);

                driver.WaitUntilVisible(By.Id("cred_password_inputtext"),
                    new TimeSpan(0, 0, 2),
                    d =>
                    {
                        d.FindElement(By.Id("cred_password_inputtext")).SendKeys(password.ToUnsecureString());

                        // Pause for validation (just in case)
                        Thread.Sleep(500);

                        d.ClickWhenAvailable(By.Id("cred_sign_in_button"), new TimeSpan(0, 0, 2));

                        d.WaitForPageToLoad();
                    },
                    d =>
                    {
                        redirectAction?.Invoke(new LoginRedirectEventArgs(username, password, d));

                        redirect = true;

                    });
            }

            return redirect ? LoginResult.Redirect : LoginResult.Success;
        }
    }
}