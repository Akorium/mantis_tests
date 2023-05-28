using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    public class ProjectRemovalTests : AuthorizationTestBase
    {
        private readonly int _projectInDB = 0;
        [Test]
        public void ProjectRemovalTest()
        {
            List<ProjectData> oldProjects = applicationManager.ProjectHelper.CheckProject(_projectInDB);
            ProjectData projectToRemove = oldProjects[_projectInDB];
            applicationManager.ProjectHelper.RemoveProject(projectToRemove.Id);
            List<ProjectData> newProjects = ProjectData.GetProjects();
            oldProjects.RemoveAt(_projectInDB);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
