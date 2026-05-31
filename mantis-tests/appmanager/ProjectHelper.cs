using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mantis_tests.Mantis;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper
    {
        private ApplicationManager manager;
        private IWebDriver driver;
        private AccountData admin;

        public ProjectHelper(ApplicationManager manager, AccountData adminAccount)
        {
            this.manager = manager;
            this.driver = manager.driver;
            this.admin = adminAccount;
        }

        public List<ProjectData> GetProjectList()
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();
            var soapProjects = client.mc_projects_get_user_accessible(admin.Name, admin.Password);

            List<ProjectData> projects = new List<ProjectData>();
            foreach (var p in soapProjects)
            {
                projects.Add(new ProjectData()
                {
                    Name = p.name,
                    Description = p.description
                });
            }
            return projects;
        }

        public void CreateProject(ProjectData project)
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();
            Mantis.ProjectData soapProject = new Mantis.ProjectData()
            {
                name = project.Name,
                description = project.Description
            };
            client.mc_project_add(admin.Name, admin.Password, soapProject);
        }

        public void RemoveProject(ProjectData project)
        {
            OpenManagePage();
            OpenManageProjectsPage();
            OpenProjectDetails(project.Name);
            ClickDeleteProjectButton();
            ConfirmDeletion();
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