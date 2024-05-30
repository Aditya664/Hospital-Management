using Hospital.DAL.Common;
using Hospital.DAL.DTO;
using Hospital.DAL.Interfaces;
using Hospital.DAL.Models;
using System.Net;


namespace Hospital.BLL.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResponse<string>> AddPatient(PatientDto patient)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            if(patient == null)
            {
                response.Success = false;
                response.ErrorMessage = "Invalid patient information provided.";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                var result = await _repository.AddPatient(patient);
                if (result == 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "An error occurred while adding the patient.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<ApiResponse<IEnumerable<Patient>>> GetAllPatients()
        {
            ApiResponse<IEnumerable<Patient>> response = new ApiResponse<IEnumerable<Patient>>();
            try
            {
                {
                    var patients = await _repository.GetAllPatient();
                    response.Data = patients;
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<ApiResponse<Patient>> GetDoctorById(int patientId)
        {
            ApiResponse<Patient> response = new ApiResponse<Patient>();
            try
            {
                if (patientId > 0)
                {
                    var patient = await _repository.GetPatientById(patientId);
                    if (patient != null)
                    {
                        response.Data = patient;
                        response.StatusCode = HttpStatusCode.OK;
                        return response;
                    }
                    else
                    {
                        response.Success = false;
                        response.ErrorMessage = $"Unable to find Patient with PatientId: {patientId}.";
                        response.StatusCode = HttpStatusCode.NotFound;
                        return response;
                    }
                }
                else
                {
                    response.ErrorMessage = "Invalid Patient Id.";
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<ApiResponse<Patient>> GetPatientByIdSSN(int PatientSSN)
        {
            ApiResponse<Patient> response = new ApiResponse<Patient>();
            try
            {
                if (PatientSSN > 0)
                {
                    var patient = await _repository.GetPatientBySSN(PatientSSN);
                    if (patient != null)
                    {
                        response.Data = patient;
                        response.StatusCode = HttpStatusCode.OK;
                        return response;
                    }
                    else
                    {
                        response.Success = false;
                        response.ErrorMessage = $"Unable to find Patient with PatientSSN: {PatientSSN}.";
                        response.StatusCode = HttpStatusCode.NotFound;
                        return response;
                    }
                }
                else
                {
                    response.ErrorMessage = "Invalid Patient SSN.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<ApiResponse<string>> UpdatePatient(int patientId, PatientDto patient)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            if (patient == null)
            {
                response.Success = false;
                response.ErrorMessage = "Invalid Patient information provided.";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                var result = await _repository.UpdatePatient(patientId, patient);
                if (result == 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }
                else if (result == 2)
                {
                    response.Success = false;
                    response.ErrorMessage = $"Unable to find Patient with Id : {patient}";
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "An error occurred while updating the Patient.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
