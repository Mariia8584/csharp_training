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
        public string soapURL;

        public ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost";
            soapURL = "http://localhost/mantisbt-2.28.3/api/soap/mantisconnect.php";

            Login = new LoginHelper(this);
            AccountData admin = new AccountData("administrator", "root");
            Project = new ProjectHelper(this, admin);
            driver.Url = "http://localhost/mantisbt-2.28.3/login_page.php";
            API = new ProjectAPIHelper(this);
        }

        public void Stop()
        {
            driver.Quit();
        }
        public LoginHelper Login { get; set; }
        public ProjectHelper Project { get; set; }
        public ProjectAPIHelper API { get; set; }
    }
}
