using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using KnowledgeBaseEmployees.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KnowledgeBaseEmployees.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        public IProjectRepository _projectRepository;
        public IEmployeeRepository _employeeRepository;
        public ITechnologyRepository _technologyRepository;

        public ProjectsController(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, ITechnologyRepository technologyRepository)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _technologyRepository = technologyRepository;
        }

        [HttpGet]
        public IEnumerable<ProjectResponse> GetAll()
        {
            return _projectRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var e = _projectRepository.GetBy(id);
            if (e == null)
            {
                return NotFound();
            }
            return new ObjectResult(e);
        }

        [HttpGet("{id}/employees")]
        public IActionResult GetProjectEmployees(int id)
        {
            var e = _employeeRepository.GetByPojectId(id);
            if (e == null)
            {
                return NotFound();
            }
            return new ObjectResult(e);
        }

        [HttpGet("search")]
        public IEnumerable<ProjectResponse> SearchBy([FromQuery] SearchQuery psq)
        {
            var e = _projectRepository.SearchBy(psq);
            return e;

        }

        [HttpGet("{id}/technologies")]
        public IActionResult GetProjectTechnology(int id)
        {
            var e = _technologyRepository.GetProjectTechnology(id);
            if (e == null)
            {
                return NotFound();
            }
            return new ObjectResult(e);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Project item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _projectRepository.Add(item);
            return CreatedAtRoute(new { Controller = "Projects", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Project item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var project = _projectRepository.GetBy(id);
            if (project == null)
            {
                return NotFound();
            }
            _projectRepository.Update(item);
            return new NoContentResult();
        }

        [HttpPut("{id}/employees/{employeeId}")]
        public IActionResult UpdateProjectEmployee(int id, int employeeId)
        {
            var project = _projectRepository.GetBy(id);
            if (project == null)
            {
                return NotFound();
            }
            _projectRepository.UpdateEmployee(id, employeeId);
            return new NoContentResult();
        }

        [HttpPut("{id}/technologies/{technologyId}")]
        public IActionResult UpdateProjectTechnology(int id, int technologyId)
        {
            var project = _projectRepository.GetBy(id);
            if (project == null)
            {
                return NotFound();
            }
            _projectRepository.UpdateTechnology(id, technologyId);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectRepository.Remove(id);
            return Ok(id);
        }

        [HttpDelete("{id}/employees/{employeeId}")]
        public IActionResult DeleteEmployee(int id, int employeeId)
        {
            _employeeRepository.RemoveProjectEmployee(id, employeeId);
            return Ok(id);
        }

        [HttpDelete("{id}/technologies/{technologyId}")]
        public IActionResult DeleteTechnology(int id, int technologyId)
        {
            _technologyRepository.RemoveProjectTechnology(id, technologyId);
            return Ok(id);
        }
    }
}
