using System;
using System.Collections.Generic;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData
            {
                summary = issueData.Summary,
                description = issueData.Description,
                category = issueData.Category,
                project = new Mantis.ObjectRef
                {
                    id = project.Id.ToString()
                }
            };
            client.mc_issue_add(account.Name, account.Password, issue);
        }
        public List<ProjectData> GetProjectsFromAPI(AccountData account)
        {
            List<ProjectData> listOfProjects = new List<ProjectData>();
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            IList<Mantis.ProjectData> projects = client.mc_projects_get_user_accessible(account.Name, account.Password);
            foreach (Mantis.ProjectData project in projects)
            {
                listOfProjects.Add(new ProjectData()
                {
                    Name = project.name,
                    Id = Convert.ToInt32(project.id),
                    Description = project.description,
                });
            }
            return listOfProjects;
        }

        public void CreateProjectByAPI(ProjectData project, AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData newProject = new Mantis.ProjectData()
            {
                name = project.Name,
                description = project.Description
            };
            client.mc_project_add(account.Name, account.Password, newProject);
        }

        public List<ProjectData> CheckProjectByAPI(int projectInDB, AccountData account)
        {
            List<ProjectData> projects = GetProjectsFromAPI(account);
            int projectsToAdd = ++projectInDB - projects.Count;
            if (projectsToAdd > 0)
            {
                for (int i = 0; i < projectsToAdd; i++)
                {
                    CreateProjectRandomProjectByAPI(account);
                }
                return GetProjectsFromAPI(account);
            }
            return projects;
        }

        private void CreateProjectRandomProjectByAPI(AccountData account)
        {
            ProjectData project = new ProjectData(RandomDataProvider.GenerateRandomString(10))
            {
                Description = RandomDataProvider.GenerateRandomString(30)
            };
            CreateProjectByAPI(project, account);
        }

        public void RemoveProjectByAPI(int id, AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            client.mc_project_delete(account.Name, account.Password, id.ToString());
        }
    }
}
