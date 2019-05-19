using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBaseEmployees.Repository;
using KnowledgeBaseEmployees.Models;

namespace KnowledgeBaseEmployees.Controllers
{

    [Route("api/users")]
    public class UsersController : Controller
    {
        public IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            var dbUser = _userRepository.Authenticate(user.Username, user.Password);

            if (dbUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(dbUser);
        }
    }
}