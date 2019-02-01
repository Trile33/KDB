using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Repository
{
    public interface ITechnologyRepository
    {
        void Add(Technology technology);
        IEnumerable<TechnologyResponse> GetAll();
        IEnumerable<Technology> GetEmployeeTechnology(int employeeId);
        IEnumerable<Technology> GetProjectTechnology(int projectId);
        TechnologyResponse GetBy(int id);
        IEnumerable<TechnologyResponse> SearchBy(SearchQuery tsq);
        void RemoveProjectTechnology(int projectId, int technologyId);
        void RemoveEmployeeTechnology(int employeeId, int technologyId);
        void Remove(int id);
        void Update(Technology technology);
        void UpdateProject(int technologyId, int projectId);
        void UpdateEmployee(int technologyId, int employeeId);
    }
}
