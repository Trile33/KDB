using KnowledgeBaseEmployees.Models;
using KnowledgeBaseEmployees.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseEmployees.Repository
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
    }
}
