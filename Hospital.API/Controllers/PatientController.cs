using Hospital.BLL.Services;
using Hospital.DAL.Common;
using Hospital.DAL.DTO;
using Hospital.DAL.Interfaces;
using Hospital.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("GetPatientById/{PatientID}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetpatientById(int PatientID)
        {
            var patient = await _patientService.GetDoctorById(PatientID);
            return await ResponseHelper.CreateActionResult(patient);
        }

        [HttpGet("GetPatientBySSN/{PatientSSN}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPatientBySSN(long PatientSSN)
        {
            var patient = await _patientService.GetPatientByIdSSN((int)PatientSSN);
            return await ResponseHelper.CreateActionResult(patient);
        }

        [HttpGet("GetAllPatient")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patient>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllDoctors()
        {
            var patients = await _patientService.GetAllPatients();
            return await ResponseHelper.CreateActionResult(patients);
        }

        [HttpPost("AddPatient")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddDoctor([FromBody] PatientDto patient)
        {
            var result = await _patientService.AddPatient(patient);
            return await ResponseHelper.CreateActionResult(result);
        }


        [HttpPut("UpdatePatient/{PatientId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePatient(int PatientId, [FromBody] PatientDto patient)
        {
            var result = await _patientService.UpdatePatient(PatientId, patient);
            return await ResponseHelper.CreateActionResult(result);
        }

    }
}
