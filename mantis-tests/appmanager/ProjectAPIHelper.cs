using mantis_tests.Mantis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectAPIHelper : HelperBase
    {
        private string baseUrl;

        public ProjectAPIHelper(ApplicationManager manager) : base(manager) { }

        public List<ProjectData> GetProjects(AccountData account)
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();
            var projects = client.mc_projects_get_user_accessible(account.Name, account.Password);
            List<ProjectData> projectList = new List<ProjectData>();
            foreach (var p in projects)
            {
                ProjectData project = new ProjectData();
                project.Name = p.name;
                project.Description = p.description;
                projectList.Add(project);
            }
            return projectList;
        }

        public void AddProject(AccountData account, ProjectData projectData)
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            project.description = projectData.Description;
            client.mc_project_add(account.Name, account.Password, project);
        }
    }
}

