using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    public class ProjectRemovalTests : AuthorizationTestBase
    {
        private readonly int _projectInList = 5;
        [Test]
        public void ProjectRemovalTest()
        {
            List<ProjectData> oldProjects = applicationManager.ProjectHelper.CheckProject(_projectInList);
            ProjectData projectToRemove = oldProjects[_projectInList];
            applicationManager.ProjectHelper.RemoveProject(projectToRemove.Id);
            List<ProjectData> newProjects = ProjectData.GetProjects();
            oldProjects.RemoveAt(_projectInList);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
        [Test]
        public void ProjectRemovalTestByAPI()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "12345678"
            };
            List<ProjectData> oldProjects = applicationManager.APIHelper.CheckProjectByAPI(_projectInList, account);
            ProjectData projectToRemove = oldProjects[_projectInList];
            applicationManager.APIHelper.RemoveProjectByAPI(projectToRemove.Id, account);
            List<ProjectData> newProjects = applicationManager.APIHelper.GetProjectsFromAPI(account);
            oldProjects.RemoveAt(_projectInList);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
