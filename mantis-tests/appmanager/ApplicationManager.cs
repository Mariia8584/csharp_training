using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using static System.Net.WebRequestMethods;

namespace mantis_tests
{
    public class ApplicationManager
    {
        public IWebDriver driver;
        public string baseURL;

        public ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost";

            Login = new LoginHelper(this);
            Project = new ProjectHelper(this);

            driver.Url = "http://localhost/mantisbt-2.28.3/login_page.php";
        }

        public void Stop()
        {
            driver.Quit();
        }
        public LoginHelper Login { get; set; }
        public ProjectHelper Project { get; set; }
    }
}
