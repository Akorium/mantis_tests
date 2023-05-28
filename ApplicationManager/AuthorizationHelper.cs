using OpenQA.Selenium;

namespace mantis_tests
{
    public class AuthorizationHelper : HelperBase
    {
        public AuthorizationHelper(ApplicationManager manager) : base(manager) { }
        public void LogIn(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.XPath("//input[@value='Вход']")).Click();
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Вход']")).Click();
        }
        public void LogOut() 
        {
            driver.FindElement(By.XPath("//div[@id='navbar-container']/div[2]/ul/li[3]/a/span")).Click();
            driver.FindElement(By.LinkText("Выход")).Click();
        }
    }
}
