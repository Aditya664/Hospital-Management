using Hospital.DAL.DTO;
using Hospital.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatient();
        Task<Patient> GetPatientById(int patientId);
        Task<Patient> GetPatientBySSN(long patientSSN);
        Task<int> AddPatient(PatientDto patient);
        Task<int> UpdatePatient(int patientId, PatientDto patient);
    }
}
