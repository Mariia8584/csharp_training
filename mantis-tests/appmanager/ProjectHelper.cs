using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper
    {
        private ApplicationManager manager;
        private IWebDriver driver;

        public ProjectHelper(ApplicationManager manager)
        {
            this.manager = manager;
            this.driver = manager.driver;
        }

        public void CreateProject(ProjectData project)
        {
            OpenManagePage();
            OpenManageProjectsPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        public void RemoveProject(ProjectData project)
        {
            OpenManagePage();
            OpenManageProjectsPage();
            OpenProjectDetails(project.Name);
            ClickDeleteProjectButton();
            ConfirmDeletion();
        }

        public List<ProjectData> GetProjectList()
        {
            OpenManagePage();
            OpenManageProjectsPage();

            List<ProjectData> projects = new List<ProjectData>();

            var rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            foreach (var row in rows)
            {
                string name = row.FindElement(By.TagName("td")).Text;
                if (name != "Название" && name != "General")
                {
                    projects.Add(new ProjectData() { Name = name });
                }
            }

            return projects;
        }

        public void OpenManagePage()
        {
            driver.FindElement(By.CssSelector("a[href='/mantisbt-2.28.3/manage_overview_page.php']")).Click();
        }

        public void OpenManageProjectsPage()
        {
            driver.FindElement(By.CssSelector("a[href='/mantisbt-2.28.3/manage_proj_page.php']")).Click();
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.CssSelector("button.btn-primary.btn-white.btn-round")).Click();
        }

        public void FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            if (project.Description != null)
            {
                driver.FindElement(By.Id("project-description")).SendKeys(project.Description);
            }
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector("input.btn-primary.btn-white.btn-round")).Click();
        }

        public void OpenProjectDetails(string projectName)
        {
            driver.FindElement(By.XPath($"//a[contains(@href, 'manage_proj_edit_page.php') and contains(text(), '{projectName}')]")).Click();
        }

        public void ClickDeleteProjectButton()
        {
            driver.FindElement(By.CssSelector("button[formaction='manage_proj_delete.php']")).Click();
        }

        public void ConfirmDeletion()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//form[@method='post']")));

            var confirmButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@type='submit' and @value='Удалить проект']")));
            confirmButton.Click();
        }
    }
}