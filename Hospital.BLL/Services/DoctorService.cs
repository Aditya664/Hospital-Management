using Hospital.DAL.Common;
using Hospital.DAL.DTO;
using Hospital.DAL.Interfaces;
using Hospital.DAL.Models;
using Hospital.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Services
{
    public class DoctorService : IDoctorService
    {
        public readonly IDoctorRepository _repository;
        public DoctorService(IDoctorRepository repository) 
        {
            _repository = repository;
        }

        public async Task<ApiResponse<string>> AddDoctor(DoctorDto doctor)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            if (doctor == null)
            {
                response.Success = false;
                response.ErrorMessage = "Invalid Doctor information provided.";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                var result = await _repository.AddDoctor(doctor);
                if (result == 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "An error occurred while adding the doctor.";
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

        public async Task<ApiResponse<IEnumerable<Doctor>>> GetAllDoctors(int pageNumber, int pageSize)
        {
            ApiResponse<IEnumerable<Doctor>> response = new ApiResponse<IEnumerable<Doctor>>();
            try
            {
                {
                    var doctor = await _repository.GetAllDoctors(pageNumber, pageSize);
                    response.Data = doctor;
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

        public async Task<ApiResponse<Doctor>> GetDoctorById(int doctorId)
        {
            ApiResponse<Doctor> response = new ApiResponse<Doctor>();
            try
            {
                if (doctorId > 0)
                {
                    var doctor = await _repository.GetDoctorById(doctorId);
                    if (doctor != null)
                    {
                        response.Data = doctor;
                        response.StatusCode = HttpStatusCode.OK;
                        return response;
                    }
                    else
                    {
                        response.Success = false;
                        response.ErrorMessage = $"Unable to find Doctor with DoctorId: {doctorId}.";
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

        public async Task<ApiResponse<Doctor>> GetDoctorByIdSSN(int doctorSSN)
        {
            ApiResponse<Doctor> response = new ApiResponse<Doctor>();
            try
            {
                if (doctorSSN > 0)
                {
                    var doctor = await _repository.GetDoctorBySSN(doctorSSN);
                    if (doctor != null)
                    {
                        response.Data = doctor;
                        response.StatusCode = HttpStatusCode.OK;
                        return response;
                    }
                    else
                    {
                        response.Success = false;
                        response.ErrorMessage = $"Unable to find Doctor with DoctorSSN: {doctorSSN}.";
                        response.StatusCode = HttpStatusCode.NotFound;
                        return response;
                    }
                }
                else
                {
                    response.ErrorMessage = "Invalid Patient Id.";
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

        public async Task<ApiResponse<DoctorWithPatient>> GetDoctorWithPatientById(int patientId)
        {
            ApiResponse<DoctorWithPatient> response = new ApiResponse<DoctorWithPatient>();
            try
            {
                if (patientId > 0)
                {
                    var doctor = await _repository.GetDoctorWithPatientById(patientId);
                    if (doctor != null)
                    {
                        response.Data = doctor;
                        response.StatusCode = HttpStatusCode.OK;
                        return response;
                    }
                    else
                    {
                        response.ErrorMessage = $"Unable to find Patient with PatientID: {patientId}.";
                        response.Success = false;
                        response.StatusCode = HttpStatusCode.NotFound;
                        return response;
                    }
                }
                else
                {
                    response.ErrorMessage = "Invalid Patient Id.";
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

        public async Task<ApiResponse<string>> UpdateDoctor(int DocotrId, DoctorDto doctor)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            if (doctor == null)
            {
                response.Success = false;
                response.ErrorMessage = "Invalid Doctor information provided.";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                var result = await _repository.UpdateDoctor(DocotrId, doctor);
                if (result == 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }
                else if (result == 2)
                {
                    response.Success = false;
                    response.ErrorMessage = $"Unable to find Doctor with Id : {DocotrId}";
                    response.StatusCode = HttpStatusCode.NotFound;
                    return  response;
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "An error occurred while updating the doctor.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return  response;
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
