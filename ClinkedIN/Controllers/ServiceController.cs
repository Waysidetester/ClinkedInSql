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

        public ActionResult<SuccessfulAddedService> AddService (CreateServiceRequest serviceToAdd)
        {
            var newService = _serviceRepository.AddService(serviceToAdd.Name, serviceToAdd.Description, serviceToAdd.Price);

            return newService;
        }
    }
}