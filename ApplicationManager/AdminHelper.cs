using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private readonly string baseUrl;

        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager) 
        {
            this.baseUrl = baseUrl;
        }
        public List<AccountData> GetAccounts() 
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenMantisAndLogin();
            driver.Url = baseUrl + "/manage_user_page.php?";
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("tbody tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d$");
                string id = m.Value;
                accounts.Add(new AccountData()
                {
                    Name = name,
                    Id = id
                });
            }
            return accounts;
        }
        public void DeleteAccount(AccountData account) 
        {
            IWebDriver driver = OpenMantisAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.XPath("//input[@value='Delete User']")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Account']")).Click();
        }

        private IWebDriver OpenMantisAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver
            {
                Url = baseUrl + "/login_page.php"
            };
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type=submit]")).Click();
            driver.FindElement(By.Name("password")).SendKeys("12345678");
            driver.FindElement(By.CssSelector("input[type=submit]")).Click();
            return driver;
        }
    }
}
