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
    public interface IDoctorService
    {
        Task<ApiResponse<IEnumerable<Doctor>>> GetAllDoctors(int pageNumber, int pageSize);
        Task<ApiResponse<Doctor>> GetDoctorById(int doctorId);
        Task<ApiResponse<Doctor>> GetDoctorByIdSSN(int doctorId);
        Task<ApiResponse<DoctorWithPatient>> GetDoctorWithPatientById(int patientId);
        Task<ApiResponse<string>> AddDoctor(DoctorDto doctor);
        Task<ApiResponse<string>> UpdateDoctor(int DocotrId, DoctorDto doctor);
  
}
}
