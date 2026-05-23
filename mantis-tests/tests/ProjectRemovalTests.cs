using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : TestBase
    {
        [Test]
        public void TestRemoveProject()
        {
            List<ProjectData> oldProjects = app.Project.GetProjectList();

            if (oldProjects.Count == 0)
            {
                ProjectData testProject = new ProjectData();
                testProject.Name = "ProjectForDelete";
                app.Project.CreateProject(testProject);
                oldProjects = app.Project.GetProjectList();
            }

            ProjectData projectToRemove = oldProjects[0];
            app.Project.RemoveProject(projectToRemove);
            
            List<ProjectData> newProjects = app.Project.GetProjectList();

            Assert.AreEqual(oldProjects.Count - 1, newProjects.Count);
        }
    }
}
