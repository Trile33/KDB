using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsTeamLead { get; set; }

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public virtual ICollection<TechnologyEmployee> TechnologyEmployees { get; set; }
    }
}
