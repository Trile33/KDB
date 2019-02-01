using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseEmployees.Models
{
    public class ProjectEmployee
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

        public Project Project { get; set; }
        public Employee Employee { get; set; }
    }
}
