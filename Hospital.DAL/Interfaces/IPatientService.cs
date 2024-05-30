using Hospital.DAL.Common;
using Hospital.DAL.DTO;
using Hospital.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Interfaces
{
    public interface IPatientService
    {
        Task<ApiResponse<IEnumerable<Patient>>> GetAllPatients();
        Task<ApiResponse<Patient>> GetDoctorById(int patientId);
        Task<ApiResponse<Patient>> GetPatientByIdSSN(int patientId);
        Task<ApiResponse<string>> AddPatient(PatientDto patient);
        Task<ApiResponse<string>> UpdatePatient(int patientId, PatientDto patient);
  
}
}
