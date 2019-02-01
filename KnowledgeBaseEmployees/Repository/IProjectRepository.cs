using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Repository
{
    public interface IProjectRepository
    {
        void Add(Project project);
        IEnumerable<ProjectResponse> GetAll();
        ProjectResponse GetBy(int id);
        IEnumerable<Project> GetEmployeeProjects(int employeeId);
        IEnumerable<ProjectResponse> SearchBy(SearchQuery psq);
        void Remove(int id);
        void RemoveEmployeeProject(int employeeId, int projectId);
        void RemoveTechnologyProject(int technologyId, int projectId);
        void Update(Project project);
        void UpdateEmployee(int projectId, int employeeId);
        void UpdateTechnology(int projectId, int technologyId);
    }
}
