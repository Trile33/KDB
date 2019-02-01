using KnowledgeBaseEmployees.Data;
using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBaseEmployees.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly KnowledgeDBContext _context;

        public ProjectRepository(KnowledgeDBContext context)
        {
            _context = context;
        }

        public void Add(Project project)
        {
            _context.Add(project);
            _context.SaveChanges();
        }

        public IEnumerable<ProjectResponse> GetAll()
        {
            return _context.Projects
                .Include(p => p.ProjectEmployees)
                .Include(p => p.TechnologyProjects)
                .Select(p => new ProjectResponse(p))
                .ToList();
        }

        public ProjectResponse GetBy(int id)
        {
            var project = _context.Projects
                .Include(p => p.ProjectEmployees)
                .Include(p => p.TechnologyProjects)
                .Single(p => p.Id == id);
            return new ProjectResponse(project);
        }

        public IEnumerable<ProjectResponse> SearchBy(SearchQuery psq)
        {
            if(string.IsNullOrWhiteSpace(psq.Query))
            {
                return GetAll();
            }

            var queries = psq.Querys;
            
            var result = _context.Projects
                .Where(p =>
                    queries.Any(query => p.Name.ToLower().Contains(query)) ||
                    queries.Any(query => p.Code.ToLower().Contains(query)) ||
                    queries.Any(query => p.Description.ToLower().Contains(query))
                );

            return result == null 
                ? new List<ProjectResponse>()
                : result.Select(r => new ProjectResponse(r)).ToList();
        }

        public IEnumerable<Project> GetEmployeeProjects(int employeeId)
        {
           return _context.Projects.Where(x => x.ProjectEmployees.Any(y => y.EmployeeId == employeeId)).ToList();
        }

        public void Remove(int id)
        {
            var itemToRemove = _context.Projects.SingleOrDefault(r => r.Id == id);
            if (itemToRemove != null) { 
                _context.Projects.Remove(itemToRemove);
            }
            _context.SaveChanges();
        }

        public void RemoveEmployeeProject(int employeeId, int projectId)
        {
            var itemToRemove = _context.ProjectEmployees.Single(pe => pe.EmployeeId == employeeId && pe.ProjectId == projectId);
            _context.ProjectEmployees.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void RemoveTechnologyProject(int technologyId, int projectId)
        {
            var itemToRemove = _context.TechnolgyProjects.Single(pe => pe.TechnologyId == technologyId && pe.ProjectId == projectId);
            _context.TechnolgyProjects.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void Update(Project project)
        {
            var p = _context.Projects.SingleOrDefault(r => r.Id == project.Id);
            if (p != null)
            {
                p.Name = project.Name;
                p.Code = project.Code;
                p.Description = project.Description;
            }
            _context.SaveChanges();
        }

        public void UpdateEmployee(int projectId, int employeeId)
        {
            if (_context.ProjectEmployees.Any(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId))
            {
                throw new System.Exception("Project/Employee already linked in DB!");
            }

            _context.ProjectEmployees.Add(new ProjectEmployee {
                ProjectId = projectId,
                EmployeeId = employeeId
            });

            _context.SaveChanges();
        }

        public void UpdateTechnology(int projectId, int technologyId)
        {
            if (_context.TechnolgyProjects.Any(pe => pe.ProjectId == projectId && pe.TechnologyId == technologyId))
            {
                throw new System.Exception("Project/Technology already linked in DB!");
            }

            _context.TechnolgyProjects.Add(new TechnologyProject
            {
                ProjectId = projectId,
                TechnologyId = technologyId
            });

            _context.SaveChanges();
        }
    }
}
