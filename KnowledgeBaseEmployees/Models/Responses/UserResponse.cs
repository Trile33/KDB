using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseEmployees.Models.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
        public IList<int> EmployeeIds { get; set; } = new List<int>();

        public UserResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            Token = user.Token;
            Role = user.Role;
            EmployeeIds = user.Employees == null
               ? new List<int>()
               : user.Employees.Select(p => p.Id).ToList();
        }
    }
}
