using KnowledgeBaseEmployees.Models;
using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Repository
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        IEnumerable<EmployeeResponse> GetAll();
        EmployeeResponse Find(int id);
        IEnumerable<Employee> GetByPojectId(int projectId);
        IEnumerable<EmployeeResponse> SearchBy(SearchQuery esq);
        void Remove(int id);
        void RemoveProjectEmployee(int projectId, int employeeId);
        void RemoveTechnologyEmployee(int technologyId, int employeeId);
        void Update(Employee employee);
        void UpdateTechnology(int id, int technologyId);
        void UpdateProject(int employeeId, int projectId);
    }
}
