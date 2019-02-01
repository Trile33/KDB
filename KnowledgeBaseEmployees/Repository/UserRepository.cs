using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using KnowledgeBaseEmployees.Data;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseEmployees.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly KnowledgeDBContext _context;

        public UserRepository(KnowledgeDBContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {

            var userResp = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (userResp == null)
                return null;

            // authentication successful so return user details without password
            userResp.Password = null;
            User user = new User
            {
                Id = userResp.Id,
                Password = userResp.Password,
                Username = userResp.Username,
                Token = userResp.Token,
                Role = userResp.Role
            };

            return user;
        }
    }
}
