using NUnit.Framework;
using System;
using System.Text;

namespace mantis_tests
{
    public class TestBase
    {
        protected ApplicationManager applicationManager;
        [OneTimeSetUp]
        public void SetupApplicationManager()
        {
            applicationManager = ApplicationManager.GetInstance();
        }
        /*
        [OneTimeTearDown]
        public void TearDownApplicationManager()
        {
            applicationManager.Driver.Quit();
        }
        */
    }
}
