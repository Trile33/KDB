using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBaseEmployees.Models.Responses
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        
        public IList<int> TechnologyIds { get; set; } = new List<int>();
        public IList<int> EmployeeIds { get; set; } = new List<int>();

        public ProjectResponse(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Code = project.Code;
            Description = project.Description;
            EmployeeIds = project.ProjectEmployees == null
               ? new List<int>()
               : project.ProjectEmployees.Select(p => p.EmployeeId).ToList();

            TechnologyIds = project.TechnologyProjects == null
               ? new List<int>()
               : project.TechnologyProjects.Select(p => p.TechnologyId).ToList();
        }
    }
}
