using KnowledgeBaseEmployees.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBaseEmployees.Models
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsTeamLead { get; set; }

        public IList<int> ProjectIds { get; set; } = new List<int>();
        public IList<int> TechnologyIds { get; set; } = new List<int>();

        public EmployeeResponse(Employee employee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Username = employee.Username;
            Password = employee.Password;
            IsTeamLead = employee.IsTeamLead;

            ProjectIds = employee.ProjectEmployees == null 
               ? new List<int>()
               : employee.ProjectEmployees.Select(p => p.ProjectId).ToList();

            TechnologyIds = employee.TechnologyEmployees == null
               ? new List<int>()
               : employee.TechnologyEmployees.Select(p => p.TechnologyId).ToList();
        }
    }
}
