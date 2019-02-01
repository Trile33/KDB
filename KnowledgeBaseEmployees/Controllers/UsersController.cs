using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBaseEmployees.Repository;

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
        public IActionResult Authenticate([FromBody]string username, [FromBody]string password)
        {
            var user = _userRepository.Authenticate(username, password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}