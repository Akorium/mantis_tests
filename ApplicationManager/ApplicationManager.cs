using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper RegistrationHelper { get; set; }
        public FTPHelper FtpHelper { get; set; }
        public JamesHelper JamesHelper { get; set; }
        public MailHelper MailHelper { get; set; }
        public AuthorizationHelper AuthorizationHelper { get; set; }
        public NavigationHelper NavigationHelper { get; set; }
        public ProjectHelper ProjectHelper { get; set; }
        public RandomDataProvider RandomDataProvider { get; set; }
        public AdminHelper AdminHelper { get; set; }
        public APIHelper APIHelper { get; set; }

        private static readonly ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.25.7/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.07);
            RegistrationHelper = new RegistrationHelper(this);
            FtpHelper = new FTPHelper(this);
            JamesHelper = new JamesHelper(this);
            MailHelper = new MailHelper(this);
            AuthorizationHelper = new AuthorizationHelper(this);
            NavigationHelper = new NavigationHelper(this);
            ProjectHelper = new ProjectHelper(this);
            RandomDataProvider = new RandomDataProvider(this);
            AdminHelper = new AdminHelper(this, baseURL);
            APIHelper = new APIHelper(this);
        }
        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        public static ApplicationManager GetInstance()
        {
            if (!applicationManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.25.7/login_page.php";
                applicationManager.Value = newInstance;
            }
            return applicationManager.Value;

        }
        public IWebDriver Driver => driver;
    }
}
