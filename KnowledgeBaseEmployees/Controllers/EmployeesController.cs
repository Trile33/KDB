using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseEmployees.Controllers
{
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        public IEmployeeRepository _employeeRepository;
        public ITechnologyRepository _technologyRepository;
        public IProjectRepository _projectRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, ITechnologyRepository technologyRepository, IProjectRepository projectRepository)
        {
            _employeeRepository = employeeRepository;
            _technologyRepository = technologyRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public IEnumerable<EmployeeResponse> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var e = _employeeRepository.Find(id);
            if (e == null)
            {
                return NotFound();
            }
            return new ObjectResult(e);
        }

        [HttpGet("{id}/technologies")]
        public IActionResult GetEmployeeTechnologies(int employeeId)
        {
            var e = _technologyRepository.GetEmployeeTechnology(employeeId);
            if (e == null)
            {
                return NotFound();
            }
            return new ObjectResult(e);
        }

        [HttpGet("{id}/projects")]
        public IActionResult GetEmployeeProjects(int employeeId)
        {
            var e = _projectRepository.GetEmployeeProjects(employeeId);
            if (e == null)
            {
                return NotFound();
            }
            return new ObjectResult(e);
        }

        [HttpGet("search")]
        public IEnumerable<EmployeeResponse> SearchBy([FromQuery] SearchQuery esq)
        {
            var e = _employeeRepository.SearchBy(esq);
            return e;

        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _employeeRepository.Add(item);
            return CreatedAtRoute(new { Controller = "Employees", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var project = _employeeRepository.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            _employeeRepository.Update(item);
            return new NoContentResult();
        }

        [HttpPut("{id}/technologies/{technologyId}")]
        public IActionResult UpdateEmployeeTechnology(int id, int technologyId)
        {
            var employee = _employeeRepository.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeRepository.UpdateTechnology(id, technologyId);
            return new NoContentResult();
        }

        [HttpPut("{id}/projects/{projectId}")]
        public IActionResult UpdateEmployeeProject(int id, int projectId)
        {
            var employee = _employeeRepository.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeRepository.UpdateProject(id, projectId);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeRepository.Remove(id);
            return Ok(id);
        }

        [HttpDelete("{id}/technologies/{technologyId}")]
        public IActionResult DeleteTechnology(int id, int technologyId)
        {
            _technologyRepository.RemoveEmployeeTechnology(id, technologyId);
            return Ok(id);
        }

        [HttpDelete("{id}/projects/{projectId}")]
        public IActionResult DeleteProject(int id, int projectId)
        {
            _projectRepository.RemoveEmployeeProject(id, projectId);
            return Ok(id);
        }
    }
}
