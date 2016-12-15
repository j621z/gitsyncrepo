﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Xrm.UITests.Api
{
    public static class StringExtensions
    {
        public static SecureString ToSecureString(this string @string)
        {
            var secureString = new SecureString();

            if (@string.Length > 0)
            {
                foreach (var c in @string.ToCharArray())
                    secureString.AppendChar(c);
            }

            return secureString;
        }

        public static string ToUnsecureString(this SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }

    public static class BoolExtensions
    {
        public static string AsString(this bool @bool, string trueCondition, string falseCondition)
        {
            return @bool ? trueCondition : falseCondition;
        }

        public static string IfFalse(this bool @bool, string falseCondition)
        {
            return AsString(@bool, string.Empty, falseCondition);
        }

        public static string IfTrue(this bool @bool, string trueCondition)
        {
            return AsString(@bool, trueCondition, string.Empty);
        }
    }
}