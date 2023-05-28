using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    public class ProjectCreationTests : AuthorizationTestBase
    {
        public static IEnumerable<ProjectData> RandomProjectProvider()
        {
            List<ProjectData> project = new List<ProjectData>
            {
                new ProjectData(RandomDataProvider.GenerateRandomString(10))
                {
                    Description = RandomDataProvider.GenerateRandomString(30)
                }
            };
            return project;
        }
        [Test, TestCaseSource("RandomProjectProvider")]
        public void ProjectCreationTest(ProjectData project)
        {
            List<ProjectData> oldProjects = ProjectData.GetProjects();
            applicationManager.ProjectHelper.CreateProject(project);
            List<ProjectData> newProjects = ProjectData.GetProjects();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
