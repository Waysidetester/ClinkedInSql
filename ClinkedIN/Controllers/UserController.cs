using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewUser(UserConnections addUserRequest)
        {
            var newUser = addUserRequest.AddNewUser();
            return Accepted($"api/user/{newUser.Id}", newUser);
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = new UserConnections().GetAllUsers();
            return Accepted($"api/user", users);
        }

        [HttpGet("{id}")]
        public ActionResult GetSpecificUser(int id)
        {
            var user = new UserConnections().GetSpecificUser(id);
            return Accepted($"api/user/{id}", user);
        }

        [HttpGet("{id}&warden={name}")]
        public ActionResult GetUsersAsWarden(int id, string name)
        {
            var user = new UserConnections().GetSpecificUser(id);
            if (name == user.Name)
            {
                var users = new UserConnections().GetUsersAsWarden(user.IsPrisoner);
                return Accepted(users);
            }
            return Accepted();
        }

        [HttpGet("{id}")]
        public ActionResult<MemberWithDescriptions> GetMember(int id)
        {
            return new MemberWithDescriptions();
        } //_memberRepo.GetMember(id).ConvertInterests();

        [HttpGet("{id}/release")]
        public ActionResult GetReleaseDays(int id)
        {
            throw new Exception();  
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = new UserConnections().DeleteUser(id);
            return Accepted(user);
        }

        [HttpGet("{id}&details")]
        public ActionResult GetUserWithDetails(int id)
        {
            var user = new UserConnections().GetUserWithDetails(id);
            return Accepted(user);
        }
    }
}
