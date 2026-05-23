using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
namespace mantis_tests
{
    public class LoginHelper
    {
        private ApplicationManager manager;
        private IWebDriver driver;

        public LoginHelper(ApplicationManager manager)
        {
            this.manager = manager;
            this.driver = manager.driver;
        }

        public void LoginAsAdmin()
        {
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }
    }
}
