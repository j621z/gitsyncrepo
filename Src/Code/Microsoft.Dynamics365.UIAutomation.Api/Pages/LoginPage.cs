﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Security;
using System.Threading;

namespace Microsoft.Dynamics365.UIAutomation.Api
{
    public enum LoginResult
    {
        Success,
        Failure,
        Redirect
    }

    /// <summary>
    /// Login Page
    /// </summary>
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
        /// <summary>
        /// Login Page
        /// </summary>
        /// <param name="uri">The Uri</param>
        /// <param name="username">The Username to login to CRM application</param>
        /// <param name="password">The Password to login to CRM application</param>
        /// <example>xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);</example>
        public BrowserCommandResult<LoginResult> Login(Uri uri, SecureString username, SecureString password)
        {
            return this.Execute("Login", this.Login, uri, username, password, default(Action<LoginRedirectEventArgs>));
        }

        /// <summary>
        /// Login Page
        /// </summary>
        /// <param name="uri">The Uri</param>
        /// <param name="username">The Username to login to CRM application</param>
        /// <param name="password">The Password to login to CRM application</param>
        /// <param name="redirectAction">The RedirectAction</param>
        /// <example>xrmBrowser.LoginPage.Login(_xrmUri, _username, _password, ADFSLogin);</example>
        public BrowserCommandResult<LoginResult> Login(Uri uri, SecureString username, SecureString password, Action<LoginRedirectEventArgs> redirectAction)
        {
            return this.Execute("Login", this.Login, uri, username, password, redirectAction);
        }

        private LoginResult Login(IWebDriver driver, Uri uri, SecureString username, SecureString password, Action<LoginRedirectEventArgs> redirectAction)
        {
            var redirect = false;
            bool online = !(this.OnlineDomains != null && !this.OnlineDomains.Any(d => uri.Host.EndsWith(d)));
            bool redirectToNewPassword = false;
            driver.Navigate().GoToUrl(uri);

            if (online)
            {
                if (driver.IsVisible(By.Id("use_another_account_link")))
                    driver.ClickWhenAvailable(By.Id("use_another_account_link"));

                if (driver.IsVisible(By.XPath(Elements.Xpath[Reference.Login.UserId])))
                {
                    driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Login.UserId]),
                        $"The Office 365 sign in page did not return the expected result and the user '{username}' could not be signed in.");

                    driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.UserId])).SendKeys(username.ToUnsecureString());
                    driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.UserId])).SendKeys(Keys.Tab);
                    driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.UserId])).SendKeys(Keys.Enter);
                    redirectToNewPassword = true;
                    Thread.Sleep(2000);
                }
                else
                {
                    driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Login.OldSignInUserId]),
                        $"The Office 365 sign in page did not return the expected result and the user '{username}' could not be signed in.");

                    driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.OldSignInUserId])).SendKeys(username.ToUnsecureString());
                    driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.OldSignInUserId])).SendKeys(Keys.Tab);
                }

                    //If expecting redirect then wait for redirect to trigger
                if (redirectAction != null)
                {
                    //Wait for redirect to occur.
                    Thread.Sleep(3000);

                    redirectAction?.Invoke(new LoginRedirectEventArgs(username, password, driver));

                    redirect = true;
                }
                else
                {
                    if (redirectToNewPassword)
                    {
                        driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.Password])).SendKeys(password.ToUnsecureString());
                        driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.Password])).SendKeys(Keys.Tab);
                        driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.Password])).Submit();

                        // wait for button for stay signed in
                        if (driver.IsVisible(By.XPath(Elements.Xpath[Reference.Login.StaySignedIn])))
                        {
                            driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Login.StaySignedIn]),
                                new TimeSpan(0, 0, 60),
                                $"The Office 365 sign in page did not return the expected result and the user '{username}' could not be signed in.");

                            driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.StaySignedIn])).Submit();

                            Thread.Sleep(2000);
                        }
                    }
                    else
                    {
                        driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.OldSignInPassword])).SendKeys(password.ToUnsecureString());
                        driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.OldSignInPassword])).SendKeys(Keys.Tab);
                        driver.FindElement(By.XPath(Elements.Xpath[Reference.Login.OldSignInPassword])).Submit();
                    }

                    driver.WaitUntilVisible(By.XPath(Elements.Xpath[Reference.Login.CrmMainPage])
                        , new TimeSpan(0, 0, 60),
                        e => { e.WaitForPageToLoad(); },
                        f => { throw new Exception("Login page failed."); });
                }
            }

            return redirect ? LoginResult.Redirect : LoginResult.Success;
        }
    }
}
 