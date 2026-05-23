using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace mantis_tests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            app = new ApplicationManager();
            app.Login.LoginAsAdmin();
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Stop();
        }
    }
}
