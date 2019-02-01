using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KnowledgeBaseEmployees.Data;
using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;

namespace KnowledgeBaseEmployees.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly KnowledgeDBContext _context;

        public EmployeeRepository(KnowledgeDBContext context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }       

        public IEnumerable<EmployeeResponse> GetAll()
        {
            var result = _context.Employees
                .Include(e => e.ProjectEmployees)
                .Include(e => e.TechnologyEmployees)
                .Select(e => new EmployeeResponse(e))
                .ToList();
            return result;
        }


        public EmployeeResponse Find(int id)
        {
            var employee = _context.Employees
                .Include(e => e.ProjectEmployees)
                .Include(e => e.TechnologyEmployees)
                .Single(x => x.Id == id);
            return new EmployeeResponse(employee);
        }

        public IEnumerable<Employee> GetByPojectId(int projectId)
        {
            return _context.Employees.Where(x => x.ProjectEmployees.Any(y => y.ProjectId == projectId)).ToList();
        }

        public IEnumerable<EmployeeResponse> SearchBy(SearchQuery esq)
        {
            if (string.IsNullOrWhiteSpace(esq.Query))
            {
                return GetAll();
            }

            var queries = esq.Querys;

            var result = _context.Employees
                .Where(p =>
                    queries.Any(query => p.FirstName.ToLower().Contains(query)) ||
                    queries.Any(query => p.LastName.ToLower().Contains(query))
                );

            return result == null
                ? new List<EmployeeResponse>()
                : result.Select(r => new EmployeeResponse(r)).ToList();
        }

        public void Remove(int id)
        {
            var itemToRemove = _context.Employees.SingleOrDefault(r => r.Id == id);
            if (itemToRemove != null)
            {
                _context.Employees.Remove(itemToRemove);
            }
            _context.SaveChanges();
        }

        public void RemoveProjectEmployee(int projectId, int employeeId)
        {
            var itemToRemove = _context.ProjectEmployees.Single(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
            _context.ProjectEmployees.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void RemoveTechnologyEmployee(int technologyId, int employeeId)
        {
            var itemToRemove = _context.TechnologyEmployees.Single(pe => pe.TechnologyId == technologyId && pe.EmployeeId == employeeId);
            _context.TechnologyEmployees.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            var e = _context.Employees.SingleOrDefault(r => r.Id == employee.Id);
            if (e != null)
            {
                e.FirstName = employee.FirstName;
                e.LastName = employee.LastName;
                e.Username = employee.Username;
                e.Password = employee.Password;
                e.IsTeamLead = employee.IsTeamLead;
            }
            _context.SaveChanges();
        }

        public void UpdateTechnology(int employeeId, int technologyId)
        {
            if (_context.TechnologyEmployees.Any(pe => pe.EmployeeId == employeeId && pe.TechnologyId == technologyId))
            {
                throw new System.Exception("Employee/Technology already linked in DB!");
            }

            _context.TechnologyEmployees.Add(new TechnologyEmployee
            {
                EmployeeId = employeeId,
                TechnologyId = technologyId
            });

            _context.SaveChanges();
        }

        public void UpdateProject(int employeeId, int projectId)
        {
            if (_context.ProjectEmployees.Any(pe => pe.EmployeeId == employeeId && pe.ProjectId == projectId))
            {
                throw new System.Exception("Employee/Project already linked in DB!");
            }

            _context.ProjectEmployees.Add(new ProjectEmployee
            {
                EmployeeId = employeeId,
                ProjectId = projectId
            });

            _context.SaveChanges();
        }
    }
}
