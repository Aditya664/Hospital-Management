

using Hospital.DAL.DTO;
using Hospital.DAL.Exceptions;
using Hospital.DAL.Interfaces;
using Hospital.DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Hospital.DAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly string _connectionString;

        public DoctorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddDoctor(DoctorDto doctor)
        {
            try { 
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand("AddNewDoctor",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DoctorSSN", SqlDbType.VarChar).Value = doctor.DoctorSSN;
                        command.Parameters.Add("@DoctorName", SqlDbType.VarChar).Value = doctor.DoctorName;
                        command.Parameters.Add("@DoctorPhoneNo", SqlDbType.VarChar).Value = doctor.DoctorPno;

                        SqlParameter returnValue = new SqlParameter("@ReturnValue",SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnValue);
                        connection.Open();
                        command.ExecuteNonQuery();

                        return (int) returnValue.Value;
                    }
                }
            }catch (Exception ex)
            {
                throw new SystemErrorException("Something went wrong while adding new doctor"+ ex);
            }
        }

     

        public async Task<IEnumerable<Doctor>> GetAllDoctors(int pageNumber ,int pageSize)
        {
            try
            {
                List<Doctor> doctors = new List<Doctor>();
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand("GetDoctorsPaged", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PageNumber",SqlDbType.Int).Value = pageNumber;
                        command.Parameters.Add("@PageSize",SqlDbType.Int).Value = pageSize;
                        connection.Open();
                        using (SqlDataReader reader =  command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                doctors.Add(new Doctor
                                {
                                    DoctorId = reader.GetInt32(reader.GetOrdinal("doctorId")),
                                    DoctorName = reader.GetString(reader.GetOrdinal("doctorName")),
                                    DoctorSSN = reader.GetString(reader.GetOrdinal("doctorSSN")),
                                    DoctorPno = reader.GetInt64(reader.GetOrdinal("doctorPno")),
                                });
                            }
                        }
                    }
                    return doctors;
                }
            }catch (Exception ex)
            {
                throw new SystemErrorException("Something went wrong while fetching doctors.", ex);
            }

        }

        public async Task<Doctor> GetDoctorById(int doctorId)
        {
            try
            {
                Doctor doctor = null;
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand("GetDoctorById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("DoctorId", SqlDbType.Int).Value = doctorId;
                        connection.Open();
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    doctor = new Doctor
                                    {
                                        DoctorId = reader.GetInt32(reader.GetOrdinal("doctorId")),
                                        DoctorName = reader.GetString(reader.GetOrdinal("doctorName")),
                                        DoctorSSN = reader.GetString(reader.GetOrdinal("doctorSSN")),
                                        DoctorPno = reader.GetInt64(reader.GetOrdinal("doctorPno")),
                                    };
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    return doctor;
                }
            } catch (Exception ex)
            {
                throw new NotFoundException("Something went wrong while fetching doctor.", ex);
            }
        }

        public async Task<Doctor> GetDoctorBySSN(long doctorSSN)
        {
            try
            {
                Doctor doctor = null;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetDoctorBySSN", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DoctorSSN", SqlDbType.BigInt).Value = doctorSSN;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    doctor = new Doctor
                                    {
                                        DoctorId = reader.GetInt32(reader.GetOrdinal("doctorId")),
                                        DoctorName = reader.GetString(reader.GetOrdinal("doctorName")),
                                        DoctorSSN = reader.GetString(reader.GetOrdinal("doctorSSN")),
                                        DoctorPno = reader.GetInt64(reader.GetOrdinal("doctorPno")),
                                    };
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    return doctor;
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Something went wrong while fetching doctor.", ex);
            }
        }


        public async Task<DoctorWithPatient> GetDoctorWithPatientById(int PatientID)
        {
            try
            {
                DoctorWithPatient doctor = null;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetDoctorInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Patient_ID", SqlDbType.Int).Value = PatientID;
                        command.Parameters.Add("@message", SqlDbType.VarChar).Value = "";
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    doctor = new DoctorWithPatient
                                    {
                                        PatientId = reader.GetInt32(reader.GetOrdinal("Patient_ID")),
                                        PatientName = reader.GetString(reader.GetOrdinal("Name")),
                                        DoctorName = reader.GetString(reader.GetOrdinal("Doctor_Name")),
                                        PhoneNumber = reader.GetInt64(reader.GetOrdinal("Doctor_Phone_Num"))
                                    };
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                return doctor;
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Something went wrong while fetching doctors.", ex);
            }
        }

        public async Task<int> UpdateDoctor(int DoctorId, DoctorDto doctor)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand("UpdateDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DoctorId", SqlDbType.Int).Value = DoctorId;
                        command.Parameters.Add("@DoctorSSN", SqlDbType.VarChar).Value = doctor.DoctorSSN;
                        command.Parameters.Add("@DoctorName", SqlDbType.VarChar).Value = doctor.DoctorName;
                        command.Parameters.Add("@DoctorPhoneNo", SqlDbType.VarChar).Value = doctor.DoctorPno;
                        SqlParameter returnValue = new SqlParameter("@ReturnValue",SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnValue);
                        connection.Open();

                        command.ExecuteNonQuery();
                        return (int) returnValue.Value; 

                    }
                }
            }catch (Exception ex)
            {
                throw new SystemErrorException("Something went wrong while updating doctor" + ex);
            }
        }
    }
}
