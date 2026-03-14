using Hospital.Application.Services;
using Hospital.Core.Entities;
using Hospital.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        public PatientController(IPatientService ps)
        {
            patientService = ps;
        }

        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient patient)
        {
            patientService.AddPatient(patient);
            return Ok("Patient Added Successfully");
        }
    }
}
