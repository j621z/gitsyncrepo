﻿using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public static class SeleniumExtensions
    {
        #region Click

        public static IWebDriver ClickndWait(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var element = driver.FindElement(by);

            if (element != null)
            {
                element.Click();
                System.Threading.Thread.Sleep((int)timeout.TotalMilliseconds);
            }

            return driver;
        }

        public static IWebElement ClickWhenAvailable(this IWebDriver driver, By by)
        {
            return ClickWhenAvailable(driver, by, Constants.DefaultTimeout);
        }

        public static IWebElement ClickWhenAvailable(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var tryUntil = DateTime.Now.Add(timeout);

            try
            {
                var element = WaitUntilAvailable(driver, by, timeout);

                element.Click();

                return element;
            }
            catch (StaleElementReferenceException)
            {
                if (DateTime.Now <= tryUntil)
                    return ClickWhenAvailable(driver, by, timeout);
            }

            return null;
        }

        #endregion Click

        #region Script Execution

        [DebuggerNonUserCode()]
        public static object ExecuteScript(this IWebDriver driver, string script, params object[] args)
        {
            var scriptExecutor = (driver as IJavaScriptExecutor);

            if (scriptExecutor == null)
                throw new InvalidOperationException(
                    $"The driver type '{driver.GetType().FullName}' does not support Javascript execution.");

            return scriptExecutor.ExecuteScript(script, args);
        }

        [DebuggerNonUserCode()]
        public static JObject GetJsonObject(this IWebDriver driver, string @object)
        {
            @object = SanitizeReturnStatement(@object);

            var results = ExecuteScript(driver, $"return JSON.stringify({@object});").ToString();

            return JObject.Parse(results);
        }

        [DebuggerNonUserCode()]
        public static JArray GetJsonArray(this IWebDriver driver, string @object)
        {
            @object = SanitizeReturnStatement(@object);

            var results = ExecuteScript(driver, $"return JSON.stringify({@object});").ToString();

            return JArray.Parse(results);
        }

        [DebuggerNonUserCode()]
        public static T GetJsonObject<T>(this IWebDriver driver, string @object)
        {
            @object = SanitizeReturnStatement(@object);

            var results = ExecuteScript(driver, $"return JSON.stringify({@object});").ToString();
            var jsSerializer = new JavaScriptSerializer();

            jsSerializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            var jsonObj = new JavaScriptSerializer().Deserialize<T>(results);

            return jsonObj;
        }

        private static string SanitizeReturnStatement(string script)
        {
            if (script.EndsWith(";"))
            {
                script = script.TrimEnd(script[script.Length - 1]);
            }

            if (script.StartsWith("return "))
            {
                script = script.TrimStart("return ".ToCharArray());
            }

            return script;
        }

        #endregion Script Execution

        #region Browser Options

        [DebuggerNonUserCode()]
        public static void ResetZoom(this IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.TagName("body"));
            element.SendKeys(Keys.Control + "0");
        }

        #endregion Browser Options

        #region Screenshot

        [DebuggerNonUserCode()]
        public static Screenshot TakeScreenshot(this IWebDriver driver)
        {
            var screenshotDriver = (driver as ITakesScreenshot);

            if (screenshotDriver == null)
                throw new InvalidOperationException(
                    $"The driver type '{driver.GetType().FullName}' does not support taking screenshots.");

            return screenshotDriver.GetScreenshot();
        }

        [DebuggerNonUserCode()]
        public static Bitmap TakeScreenshot(this IWebDriver driver, By by)
        {
            var screenshot = TakeScreenshot(driver);
            var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));

            // Measure the location of a specific element
            IWebElement element = driver.FindElement(by);
            var crop = new Rectangle(element.Location, element.Size);

            return bmpScreen.Clone(crop, bmpScreen.PixelFormat);
        }

        #endregion Screenshot

        #region Elements

        public static T GetAttribute<T>(this IWebElement element, string attributeName)
        {
            string value = element.GetAttribute(attributeName) ?? string.Empty;

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
        }

        public static string GetAuthority(this IWebDriver driver)
        {
            string url = driver.Url;                // get the current URL (full)
            Uri currentUri = new Uri(url);          // create a Uri instance of it
            string baseUrl = currentUri.Authority;  // just get the "base" bit of the URL

            return baseUrl;
        }

        public static string GetBodyText(this IWebDriver driver)
        {
            return driver.FindElement(By.TagName("body")).Text;
        }

        public static bool HasElement(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElements(by).Count > 0;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void SendKeys(this IWebElement element, string value, bool clear)
        {
            if (clear)
            {
                element.Clear();
            }

            element.SendKeys(value);
        }

        #endregion Elements

        #region Waits

        public static bool WaitFor(this IWebDriver driver, Predicate<IWebDriver> predicate)
        {
            return WaitFor(driver, predicate, Constants.DefaultTimeout);
        }

        public static bool WaitFor(this IWebDriver driver, Predicate<IWebDriver> predicate, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);

            var result = wait.Until(d => predicate(d));

            return result;
        }

        public static bool WaitForPageToLoad(this IWebDriver driver)
        {
            return WaitForPageToLoad(driver, Constants.DefaultTimeout);
        }

        public static bool WaitForPageToLoad(this IWebDriver driver, TimeSpan timeout)
        {
            object readyState = WaitForScript(driver, "if (document.readyState) return document.readyState;", timeout);

            if (readyState != null)
                return readyState.ToString().ToLower() == "complete";

            return false;
        }

        public static object WaitForScript(this IWebDriver driver, string script)
        {
            return WaitForScript(driver, script, Constants.DefaultTimeout);
        }

        public static object WaitForScript(this IWebDriver driver, string script, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);

            wait.Until((d) =>
            {
                try
                {
                    object returnValue = ExecuteScript(driver, script);

                    return returnValue;
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                catch (WebDriverException)
                {
                    return null;
                }
            });

            return null;
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by)
        {
            return WaitUntilAvailable(driver, by, Constants.DefaultTimeout, null, null);
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout)
        {
            return WaitUntilAvailable(driver, by, timeout, null, null);
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, string exceptionMessage)
        {
            return WaitUntilAvailable(driver, by, Constants.DefaultTimeout, null, d =>
            {
                throw new InvalidOperationException(exceptionMessage);
            });
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout, string exceptionMessage)
        {
            return WaitUntilAvailable(driver, by, timeout, null, d =>
            {
                throw new InvalidOperationException(exceptionMessage);
            });
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback)
        {
            return WaitUntilAvailable(driver, by, timeout, successCallback, null);
        }

        public static IWebElement WaitUntilAvailable(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback, Action<IWebDriver> failureCallback)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            bool? success;
            IWebElement returnElement = null;

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                returnElement = wait.Until(d => d.FindElement(by));

                success = true;
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            catch (WebDriverTimeoutException)
            {
                success = false;
            }

            if (success.HasValue && success.Value && successCallback != null)
                successCallback(driver);
            else if (success.HasValue && !success.Value && failureCallback != null)
                failureCallback(driver);

            return returnElement;
        }

        public static bool WaitUntilVisible(this IWebDriver driver, By by)
        {
            return WaitUntilVisible(driver, by, Constants.DefaultTimeout, null, null);
        }

        public static bool WaitUntilVisible(this IWebDriver driver, By by, TimeSpan timeout)
        {
            return WaitUntilVisible(driver, by, timeout, null, null);
        }

        public static bool WaitUntilVisible(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback)
        {
            return WaitUntilVisible(driver, by, timeout, successCallback, null);
        }

        public static bool WaitUntilVisible(this IWebDriver driver, By by, TimeSpan timeout, Action<IWebDriver> successCallback, Action<IWebDriver> failureCallback)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            bool? success;

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));

                success = true;
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            catch (WebDriverTimeoutException)
            {
                success = false;
            }

            if (success.HasValue && success.Value && successCallback != null)
                successCallback(driver);
            else if (success.HasValue && !success.Value && failureCallback != null)
                failureCallback(driver);

            return success.Value;
        }

        #endregion Waits

        #region Args / Tracing

        public static string ToTraceString(this FindElementEventArgs e)
        {
            if (e.Element != null)
            {
                return string.Format("{4} - [{0},{1}] - <{2}>{3}</{2}>", e.Element.Location.X, e.Element.Location.Y, e.Element.TagName, e.Element.Text, e.FindMethod);
            }
            else
            {
                return e.FindMethod.ToString();
            }
        }

        #endregion Args / Tracing
    }
}