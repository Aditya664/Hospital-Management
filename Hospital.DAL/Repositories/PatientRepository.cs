using Hospital.DAL.DTO;
using Hospital.DAL.Exceptions;
using Hospital.DAL.Interfaces;
using Hospital.DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;
        public PatientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddPatient(PatientDto patient)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddPatient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PatientSSN", SqlDbType.VarChar).Value = patient.PatientSsn;
                        command.Parameters.Add("@DoctorId", SqlDbType.Int).Value = patient.DoctorId;
                        command.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = patient.Name;
                        command.Parameters.Add("@PatientGender", SqlDbType.Char).Value = patient.Gender;

                        SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnValue);
                        connection.Open();
                        command.ExecuteNonQuery();

                        return (int)returnValue.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SystemErrorException("Something went wrong while adding new doctor" + ex);
            }
        }

        public async Task<IEnumerable<Patient>> GetAllPatient()
        {
            try
            {
                List<Patient> patients = new List<Patient>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetPatientData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patients.Add(new Patient()
                                {
                                    DoctorId = reader.GetInt32(reader.GetOrdinal("Doctor_ID")),
                                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    PatientId = reader.GetInt32(reader.GetOrdinal("Patient_ID")),
                                    PatientSsn = reader.GetString(reader.GetOrdinal("Patient_SSN"))
                                 });
                            }
                        }
                    }
                    return patients;
                }
            }catch(Exception ex)
            {
                throw new SystemErrorException("Something went wrong while fetching patients.", ex);
            }
        }

        public async Task<Patient> GetPatientById(int patientId)
        {
            try
            {
                Patient patient = null;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetPatientDataById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = patientId;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    patient = new Patient
                                    {
                                        DoctorId = reader.GetInt32(reader.GetOrdinal("Doctor_ID")),
                                        Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                        Name = reader.GetString(reader.GetOrdinal("Name")),
                                        PatientId = reader.GetInt32(reader.GetOrdinal("Patient_ID")),
                                        PatientSsn = reader.GetString(reader.GetOrdinal("Patient_SSN"))
                                    };
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    return patient;
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Something went wrong while fetching doctor.", ex);
            }
        }

        public async Task<Patient> GetPatientBySSN(long patientSSN)
        {
            try
            {
                Patient patient = null;
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand("GetPatientDataBySSN", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@SSN",SqlDbType.Int).Value = patientSSN;
                        connection.Open();
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                patient = new Patient
                                {
                                    DoctorId = reader.GetInt32(reader.GetOrdinal("Doctor_ID")),
                                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    PatientId = reader.GetInt32(reader.GetOrdinal("Patient_ID")),
                                    PatientSsn = reader.GetString(reader.GetOrdinal("Patient_SSN"))
                                };
                            }
                        }
                        return patient;
                    }
                }
            }catch(Exception ex)
            {
                throw new NotFoundException(ex.Message, ex);
            }
        }

        public async Task<int> UpdatePatient(int patientId, PatientDto patient)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdatePatient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DoctorId", SqlDbType.Int).Value = patient.DoctorId;
                        command.Parameters.Add("@PatientId", SqlDbType.Int).Value = patientId;
                        command.Parameters.Add("@PatientSSN", SqlDbType.VarChar).Value = patient.PatientSsn;
                        command.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = patient.Name;
                        command.Parameters.Add("@PatientGender", SqlDbType.VarChar).Value = patient.Gender;
                        SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnValue);
                        connection.Open();

                        command.ExecuteNonQuery();
                        return (int)returnValue.Value;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new SystemErrorException("Something went wrong while updating doctor" + ex);
            }
        }
    }
}
