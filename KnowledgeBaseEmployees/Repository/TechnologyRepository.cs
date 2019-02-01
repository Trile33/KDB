using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeBaseEmployees.Data;
using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseEmployees.Repository
{
    public class TechnologyRepository : ITechnologyRepository
    {
        public readonly KnowledgeDBContext _context;

        public TechnologyRepository(KnowledgeDBContext context)
        {
            _context = context;
        }

        public void Add(Technology technology)
        {
            _context.Add(technology);
            _context.SaveChanges();
        }

        public TechnologyResponse GetBy(int id)
        {
            var tech = _context.Technologies
                .Include(t => t.TechnologyEmployees)
                .Include(t => t.TechnologyProjects)
                .Single(t => t.Id == id);
            return new TechnologyResponse(tech);
        }

        public IEnumerable<Technology> GetEmployeeTechnology(int employeeId)
        {
            return _context.Technologies.Where(x => x.TechnologyEmployees.Any(y => y.EmployeeId == employeeId)).ToList();
        }

        public IEnumerable<Technology> GetProjectTechnology(int projectId)
        {
            return _context.Technologies.Where(x => x.TechnologyProjects.Any(y => y.ProjectId == projectId)).ToList();
        }

        public IEnumerable<TechnologyResponse> GetAll()
        {
            var result = _context.Technologies
                .Include(e => e.TechnologyProjects)
                .Include(e => e.TechnologyEmployees)
                .Select(e => new TechnologyResponse(e))
                .ToList();
            return result;
        }

        public IEnumerable<TechnologyResponse> SearchBy(SearchQuery tsq)
        {
            if (string.IsNullOrWhiteSpace(tsq.Query))
            {
                return GetAll();
            }

            var queries = tsq.Querys;

            var result = _context.Technologies
                .Where(p =>
                    queries.Any(query => p.Name.ToLower().Contains(query)) ||
                    queries.Any(query => p.Information.ToLower().Contains(query))
                );

            return result == null
                ? new List<TechnologyResponse>()
                : result.Select(r => new TechnologyResponse(r)).ToList();
        }

        public void Remove(int id)
        {
            var itemToRemove = _context.Technologies.SingleOrDefault(r => r.Id == id);
            _context.Technologies.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void RemoveProjectTechnology(int projectId, int technologyId)
        {
            var itemToRemove = _context.TechnolgyProjects.Single(pe => pe.ProjectId == projectId && pe.TechnologyId == technologyId);
            _context.TechnolgyProjects.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void RemoveEmployeeTechnology(int employeeId, int technologyId)
        {
            var itemToRemove = _context.TechnologyEmployees.Single(pe => pe.EmployeeId == employeeId && pe.TechnologyId == technologyId);
            _context.TechnologyEmployees.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void Update(Technology technology)
        {
            var t = _context.Technologies.SingleOrDefault(r => r.Id == technology.Id);
            if (t != null)
            {
                t.Name = technology.Name;
                t.Information = technology.Information;
            }
            _context.SaveChanges();
        }

        public void UpdateEmployee(int technologyId, int employeeId)
        {
            if (_context.TechnologyEmployees.Any(pe => pe.TechnologyId == technologyId && pe.EmployeeId == employeeId))
            {
                throw new System.Exception("Technology/Employee already linked in DB!");
            }

            _context.TechnologyEmployees.Add(new TechnologyEmployee
            {
                TechnologyId = technologyId,
                EmployeeId = employeeId
            });

            _context.SaveChanges();
        }

        public void UpdateProject(int technologyId, int projectId)
        {
            if (_context.TechnolgyProjects.Any(pe => pe.TechnologyId == technologyId && pe.ProjectId == projectId))
            {
                throw new System.Exception("Technology/Project already linked in DB!");
            }

            _context.TechnolgyProjects.Add(new TechnologyProject
            {
                TechnologyId = technologyId,
                ProjectId = projectId
            });

            _context.SaveChanges();
        }
    }
}
