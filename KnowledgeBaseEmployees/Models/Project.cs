
using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public ICollection<TechnologyProject> TechnologyProjects { get; set; }
    }
}
