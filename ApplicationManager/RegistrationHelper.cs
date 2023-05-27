using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        internal void RegisterAccount(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            String url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("realname")).SendKeys(account.Name);
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        private string GetConfirmationUrl(AccountData account)
        {
            String message = applicationManager.MailHelper.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        private void OpenRegistrationForm()
        {
            driver.FindElement(By.LinkText("Зарегистрировать новую учётную запись")).Click();
        }

        private void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@value='Зарегистрироваться']")).Click();
        }

        private void FillRegistrationForm(AccountData account)
        {
            driver.FindElements(By.Name("username"))[0].SendKeys(account.Name);
            driver.FindElements(By.Name("email"))[0].SendKeys(account.Email);
        }

        private void OpenMainPage()
        {
            applicationManager.Driver.Url = "http://localhost/mantisbt-2.25.7/login_page.php";
        }
    }
}
