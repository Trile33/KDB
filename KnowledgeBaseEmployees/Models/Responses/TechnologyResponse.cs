using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBaseEmployees.Models.Responses
{
    public class TechnologyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        
        public IList<int> ProjectIds { get; set; } = new List<int>();
        public IList<int> EmployeeIds { get; set; } = new List<int>();

        public TechnologyResponse(Technology technology)
        {
            Id = technology.Id;
            Name = technology.Name;
            Information = technology.Information;
            ProjectIds = technology.TechnologyProjects == null 
                ? new List<int>()
                : technology.TechnologyProjects.Select(p => p.ProjectId).ToList();
            EmployeeIds = technology.TechnologyEmployees == null
                ? new List<int>()
                : technology.TechnologyEmployees.Select(p => p.EmployeeId).ToList();
        }
    }
}
