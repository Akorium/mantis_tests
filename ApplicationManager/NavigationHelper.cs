using OpenQA.Selenium;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        public NavigationHelper(ApplicationManager manager) : base(manager) { }
        public void ClickSubmitButton()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        public void GoToManagePage() 
        {
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.25.7/manage_overview_page.php']")).Click();
        }
        public void GoToHomePage()
        {
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.25.7/my_view_page.php']")).Click();
        }
    }
}
