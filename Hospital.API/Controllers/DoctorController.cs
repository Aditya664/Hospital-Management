using Hospital.DAL.Common;
using Hospital.DAL.DTO;
using Hospital.DAL.Interfaces;
using Hospital.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("GetDoctorAssoWithPatient/{PatientID}")]
        [ProducesResponseType(200, Type = typeof(DoctorWithPatient))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDoctorAssoWithPatient(int PatientID)
        {
          var result = await _doctorService.GetDoctorWithPatientById(PatientID);
          return await ResponseHelper.CreateActionResult(result);
        }

        [HttpGet("GetDoctorById/{DoctorID}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDoctorById(int DoctorID)
        {
           var doctor = await _doctorService.GetDoctorById(DoctorID);
            return await ResponseHelper.CreateActionResult(doctor);
        }

        [HttpGet("GetDoctorBySSN/{DoctorSSN}")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDoctorBySSN(long DoctorSSN)
        {
            var doctor = await _doctorService.GetDoctorByIdSSN((int)DoctorSSN);
            return await ResponseHelper.CreateActionResult(doctor);
        }

        [HttpGet("GetAllDoctors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Doctor>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllDoctors(int pageNumber, int pageSize)
        {
            var doctors = await _doctorService.GetAllDoctors(pageNumber,pageSize);
            return await ResponseHelper.CreateActionResult(doctors);
        }

        [HttpPost("AddDoctor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDto doctor)
        {
            var result = await _doctorService.AddDoctor(doctor);
            return await ResponseHelper.CreateActionResult(result);
        }


        [HttpPut("UpdateDoctor/{DoctorId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateDoctor(int DoctorId, [FromBody] DoctorDto doctor)
        {
            var result = await _doctorService.UpdateDoctor(DoctorId,doctor);
            return await ResponseHelper.CreateActionResult(result);
        }

    }
}
