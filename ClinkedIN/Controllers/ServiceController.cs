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

        public ServiceController()
        {
            _serviceRepository = new ServiceRepository();
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

        [HttpGet]
        public ActionResult<List<DbService>> GetAllServices()
        {
            var allServices = _serviceRepository.GetAllServices();

            return allServices;
        }

        [HttpDelete]
        public ActionResult<DbService> DeleteService(int id)
        {

        }
    }
}