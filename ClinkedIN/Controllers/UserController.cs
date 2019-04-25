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
        public ActionResult AddNewUser(AddUser addUserRequest)
        {
            var newUser = addUserRequest.AddNewUser();
            return Accepted($"api/user/{newUser.Id}", newUser);
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
    }
}
