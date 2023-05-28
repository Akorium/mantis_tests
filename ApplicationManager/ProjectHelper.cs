using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }
        public void CreateProject(ProjectData project)
        {
            applicationManager.NavigationHelper.GoToManagePage();
            GoToProjectManagmentPage();
            applicationManager.NavigationHelper.ClickSubmitButton();
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
            driver.FindElement(By.Name("description")).SendKeys(project.Description);
            SubmitProjectCreation();
            applicationManager.NavigationHelper.GoToHomePage();
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        internal List<ProjectData> CheckProject(int projectInDB)
        {
            List<ProjectData> projects = ProjectData.GetProjects();
            int projectsToAdd = ++projectInDB - projects.Count;
            if (projectsToAdd > 0) 
            {
                for (int i = 0; i < projectsToAdd; i++) 
                {
                    CreateProjectRandomProject();
                }
                return ProjectData.GetProjects();
            }
            return projects;
        }

        private void CreateProjectRandomProject()
        {
            ProjectData project = new ProjectData(RandomDataProvider.GenerateRandomString(10))
            {
                Description = RandomDataProvider.GenerateRandomString(30)
            };
            CreateProject(project);
        }

        internal void RemoveProject(int projectId)
        {
            applicationManager.NavigationHelper.GoToManagePage();
            GoToProjectManagmentPage();
            GoToProjectPage(projectId);
            SubmitGroupRemoval();
            SubmitGroupRemoval();
            applicationManager.NavigationHelper.GoToHomePage();
        }

        private void GoToProjectPage(int projectId)
        {
            driver.FindElement(By.XPath("//a[@href='manage_proj_edit_page.php?project_id=" + projectId + "']")).Click();
        }

        private void SubmitGroupRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        private void GoToProjectManagmentPage()
        {
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.25.7/manage_proj_page.php']")).Click();
        }
    }
}
