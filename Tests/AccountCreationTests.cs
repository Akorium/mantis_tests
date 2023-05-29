using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            applicationManager.FtpHelper.BackupFile("/config/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                applicationManager.FtpHelper.Upload("/config/config_inc.php", localFile);
            }
        }
        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };

            List<AccountData> accounts = applicationManager.AdminHelper.GetAccounts();
            AccountData existingAccount =  accounts.Find(x => x.Name == account.Name);
            if (existingAccount != null) 
            {
                applicationManager.AdminHelper.DeleteAccount(existingAccount);
            }
            
            applicationManager.JamesHelper.Remove(account);
            applicationManager.JamesHelper.Add(account);

            applicationManager.RegistrationHelper.RegisterAccount(account);
        }
        [OneTimeTearDown]
        public void RestoreConfig()
        {
            applicationManager.FtpHelper.RestoreBackupFile("/config/config_inc.php");
        }
    }
}
