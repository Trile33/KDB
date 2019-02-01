using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using KnowledgeBaseEmployees.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Controllers
{
    [Route("api/technologies")]
    public class TechnologiesController : Controller
    {
        public ITechnologyRepository _technologyRepository;
        public IEmployeeRepository _employeeRepository;
        public IProjectRepository _projectRepository;

        public TechnologiesController(ITechnologyRepository technologyRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
        {
            _technologyRepository = technologyRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public IEnumerable<TechnologyResponse> GetAll()
        {
            return _technologyRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _technologyRepository.GetBy(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("{projectId}")]
        public IActionResult FindByProjectId(int projectId)
        {
            var item = _technologyRepository.GetProjectTechnology(projectId);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("search")]
        public IEnumerable<TechnologyResponse> SearchBy([FromQuery] SearchQuery tsq)
        {
            var e = _technologyRepository.SearchBy(tsq);
            return e;

        }

        [HttpPost]
        public IActionResult Create([FromBody] Technology item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _technologyRepository.Add(item);
            return CreatedAtRoute(new { Controller = "Technologies", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Technology item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var project = _technologyRepository.GetBy(id);
            if (project == null)
            {
                return NotFound();
            }
            _technologyRepository.Update(item);
            return new NoContentResult();
        }

        [HttpPut("{id}/employees/{employeeId}")]
        public IActionResult UpdateTechnologyEmployee(int id, int employeeId)
        {
            var project = _technologyRepository.GetBy(id);
            if (project == null)
            {
                return NotFound();
            }
            _technologyRepository.UpdateEmployee(id, employeeId);
            return new NoContentResult();
        }

        [HttpPut("{id}/projects/{projectId}")]
        public IActionResult UpdateTechnologyProject(int id, int projectId)
        {
            var project = _technologyRepository.GetBy(id);
            if (project == null)
            {
                return NotFound();
            }
            _technologyRepository.UpdateProject(id, projectId);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _technologyRepository.Remove(id);
            return Ok(id);
        }

        [HttpDelete("{id}/employees/{employeeId}")]
        public IActionResult DeleteEmployee(int id, int employeeId)
        {
            _employeeRepository.RemoveTechnologyEmployee(id, employeeId);
            return Ok(id);
        }

        [HttpDelete("{id}/projects/{projectId}")]
        public IActionResult DeleteProject(int id, int projectId)
        {
            _projectRepository.RemoveTechnologyProject(id, projectId);
            return Ok(id);
        }
    }
}
