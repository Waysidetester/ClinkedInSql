using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIN.Data;
using ClinkedIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        readonly ServiceRepository _serviceRepository;
        readonly MemberServiceRepo _memberServiceRepo;

        public ServiceController()
        {
            _serviceRepository = new ServiceRepository();
            _memberServiceRepo = new MemberServiceRepo();
        }

        /* To Add a service to the DB pass the following:
         * Name: a string less than 255 characters
         * Description: a string less than 255 characters
         * Price: a decimal type number with a max of 10 digits, two of which are after the decimal
         */

        [HttpPost]
        public ActionResult<DbService> AddService (CreateServiceRequest serviceToAdd)
        {
            var newService = _serviceRepository.AddService(serviceToAdd.Name, serviceToAdd.Description, serviceToAdd.Price);

            return newService;
        }

        // To Successfully execute, Run a GET request
        [HttpGet]
        public ActionResult<List<DbService>> GetAllServices()
        {
            var allServices = _serviceRepository.GetAllServices();

            return allServices;
        }

        // To delete a service, pass the service Id in the URL
        [HttpDelete("{id}")]
        public ActionResult<DbService> DeleteService(int id)
        {
            var deleteService = _serviceRepository.DeleteService(id);

            return deleteService;
        }

        [HttpPost("{id}")]
        public ActionResult<List<MemberService>> AddServiceToMember(int id, ServiceIds services)
        {
            var membServicesList = _memberServiceRepo.AddMemberServices(id, services.Ids);

            return membServicesList;
        }
        
    }
}