using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwitchLogonUserSamples
{
    [TestClass]
    public class UnitTest1
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);

        private WindowsImpersonationContext impersonationContext;

        [TestMethod]
        public void Test_Switch_to_another_account()
        {
            IntPtr userToken;
            LogonUser("account", "domain", "password", 9, 0, out userToken);

            this.impersonationContext = WindowsIdentity.Impersonate(userToken);
        }

        [TestMethod]
        public void Test_Impersonation_undo()
        {
            impersonationContext.Undo();
        }
    }
}
