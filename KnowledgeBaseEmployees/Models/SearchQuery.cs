
using System.Collections.Generic;

namespace KnowledgeBaseEmployees.Models
{
    public class SearchQuery
    {
        public string Query { get; set; }

        public IEnumerable<string> Querys => string.IsNullOrWhiteSpace(Query) 
            ? null 
            : Query.ToLower().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
    }
}
