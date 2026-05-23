using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        [Test]
        public void TestCreateProject()
        {
            ProjectData project = new ProjectData();
            project.Name = "TestProject_" + DateTime.Now.Ticks;
            project.Description = "";

            List<ProjectData> oldProjects = app.Project.GetProjectList();

            app.Project.CreateProject(project);

            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.Add(project);

            Assert.AreEqual(oldProjects.Count, newProjects.Count);
        }
    }
}