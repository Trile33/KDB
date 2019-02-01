namespace KnowledgeBaseEmployees.Models
{
    public class TechnologyEmployee
    {
        public int Id { get; set; }
        public int TechnologyId { get; set; }
        public int EmployeeId { get; set; }

        public Technology Technology { get; set; }
        public Employee Employee { get; set; }
    }
}