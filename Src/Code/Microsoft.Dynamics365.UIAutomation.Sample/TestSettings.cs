// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;

namespace Microsoft.Dynamics365.UIAutomation.Sample
{
    public static class TestSettings
    {
        public static string AccountLogicalName = "account";
        public static string AccountId = "A8A19CDD-88DF-E311-B8E5-6C3BE5A8B200";
        public static string InvalidAccountLogicalName = "accounts";

        public static string LookupField = "primarycontactid";
        public static string LookupName = "Rene Valdes (sample)";
        private static readonly string Type = System.Configuration.ConfigurationManager.AppSettings["BrowserType"].ToString();

        public static BrowserOptions Options = new BrowserOptions
        {
            BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), Type),
            PrivateMode = true,
            FireEvents = true
        };
    }
}
