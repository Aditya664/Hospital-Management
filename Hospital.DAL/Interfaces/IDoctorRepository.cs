using Hospital.DAL.DTO;
using Hospital.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctors(int pageNumber, int pageSize);
        Task<Doctor> GetDoctorById(int doctorId);
        Task<Doctor> GetDoctorBySSN(long doctorSSN);
        Task<DoctorWithPatient> GetDoctorWithPatientById(int doctorId);
        Task<int> AddDoctor(DoctorDto doctor);
        Task<int> UpdateDoctor(int DocotrId,DoctorDto doctor);
    }
}
