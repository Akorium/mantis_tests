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
        public static Random random = new Random();
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(random.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                _ = builder.Append(Convert.ToChar(32 + Convert.ToInt32(random.NextDouble() * 65)));
            }
            return builder.ToString();
        }
        [OneTimeTearDown]
        public void TearDownApplicationManager()
        {
            applicationManager.Driver.Quit();
        }
    }
}
