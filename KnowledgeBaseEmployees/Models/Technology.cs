using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Models
{
    public class Technology
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        
        public ICollection<TechnologyEmployee> TechnologyEmployees { get; set; }
        public ICollection<TechnologyProject> TechnologyProjects { get; set; }
    }
}
