using NUnit.Framework;

namespace mantis_tests
{
    public class AuthorizationTestBase : TestBase
    {
        [OneTimeSetUp]
        public void SignInMantis()
        {
            AccountData accountData = new AccountData()
            {
                Name = "administrator",
                Password = "12345678"
            };
            applicationManager.AuthorizationHelper.LogIn(accountData);
        }
        [OneTimeTearDown]
        public void SignOutMantis() 
        {
            applicationManager.AuthorizationHelper.LogOut();
        }
    }
}
    