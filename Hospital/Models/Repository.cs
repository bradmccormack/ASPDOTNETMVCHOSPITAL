using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Interfaces;
using System.Data;

using System.Data.SqlClient;

namespace Hospital.Models
{
    public class Repository : IBedRepository, IPatientRepository, IDoctorRepository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Bed
        IBed IBedRepository.GetBed(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using(var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT BedNAme, RatePerDay, BedType FROM Bed WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(!reader.Read())
                    {
                        return null;
                    }
                    return new Bed
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("BedName")),
                        RatePerDay = reader.GetDecimal(reader.GetOrdinal("RatePerDay")),
                        BedType = reader.GetString(reader.GetOrdinal("BedType"))
                    };
                }
            }
        }

        List<IBed> IBedRepository.GetBeds()
        {
            List<IBed> Beds = new List<IBed>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT Id, BedName, RatePerDay, BedType FROM Bed";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Beds.Add(new Bed
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("BedName")),
                            RatePerDay = reader.GetDecimal(reader.GetOrdinal("RatePerDay")),
                            BedType = reader.GetString(reader.GetOrdinal("BedType"))
                        });
                    }
                }
            }
            return Beds;
        }

        #endregion


        #region Patient
        IPatient IPatientRepository.GetPatient(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT Id, Name, Address, DateOfBirth, Phone, EmergencyContact, DateOfRegistration FROM Patient WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return new Patient
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
                        EmergencyContact = reader.GetString(reader.GetOrdinal("EmergencyContact")),
                        DateOfRegistration = reader.GetDateTime(reader.GetOrdinal("DateOfRegistration"))
                    };
                }
            }
        }

        List<IPatient> IPatientRepository.GetPatients()
        {
            List<IPatient> Patients = new List<IPatient>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT Id, Name, Address, DateOfBirth, Phone, EmergencyContact, DateOfRegistration FROM Patient ORDER BY Name ASC";
      
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Patients.Add(new Patient
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            EmergencyContact = reader.GetString(reader.GetOrdinal("EmergencyContact")),
                            DateOfRegistration = reader.GetDateTime(reader.GetOrdinal("DateOfRegistration"))
                        });
                    }
   
                }
            }
            return Patients;
        }

        IVisit IPatientRepository.GetVisit(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT Id, PatientType, DoctorID, PatientID, BedID, BedID, DateofVisit, DateofDischarge, Symptoms, Disease, Treatment FROM Visit WHERE Id = @id ORDER BY DateOfVisit DESC ";
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return new Visit
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        PatientType = reader.GetBoolean(reader.GetOrdinal("PatientType")),
                        DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                        PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                        BedID = reader.GetInt32(reader.GetOrdinal("BedId")),
                        DateOfVisit = reader.GetDateTime(reader.GetOrdinal("DateOfVisit")),
                        DateOfDischarge = reader.GetDateTime(reader.GetOrdinal("DateofDischarge")),
                        Symptoms = reader.GetString(reader.GetOrdinal("Symptoms")),
                        Disease = reader.GetString(reader.GetOrdinal("Disease")),
                        Treatment = reader.GetString(reader.GetOrdinal("Treatment"))
                    };
                }
            }
        }

        List<IVisit> IPatientRepository.GetVisits()
        {
            List<IVisit> PatientVisits = new List<IVisit>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT Id, PatientType, DoctorID, PatientID, BedID, BedID, DateofVisit, DateofDischarge, Symptoms, Disease, Treatment FROM Visit ORDER BY DateOfVisit DESC ";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PatientVisits.Add(new Visit
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            PatientType = reader.GetBoolean(reader.GetOrdinal("PatientType")),
                            DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                            PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                            BedID = reader.GetInt32(reader.GetOrdinal("BedId")),
                            DateOfVisit = reader.GetDateTime(reader.GetOrdinal("DateOfVisit")),
                            DateOfDischarge = reader.GetDateTime(reader.GetOrdinal("DateofDischarge")),
                            Symptoms = reader.GetString(reader.GetOrdinal("Symptoms")),
                            Disease = reader.GetString(reader.GetOrdinal("Disease")),
                            Treatment = reader.GetString(reader.GetOrdinal("Treatment"))
                        });
                    }
                }
            }
            return PatientVisits;
        }

        #endregion

        #region Doctor
        IDoctor IDoctorRepository.GetDoctor(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT Id, Name, Address, Phone FROM Doctor WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    return new Doctor
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    };
                }
            }
        }

        List<IDoctor> IDoctorRepository.GetDoctors()
        {
            List<IDoctor> Doctors = new List<IDoctor>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT Id, Name, Address, Phone FROM Doctor ORDER BY NAME ASC";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                   while(reader.Read())
                   {
                       Doctors.Add(new Doctor
                       {
                           Id = reader.GetInt32(reader.GetOrdinal("id")),
                           Name = reader.GetString(reader.GetOrdinal("Name")),
                           Address = reader.GetString(reader.GetOrdinal("Address")),
                           Phone = reader.GetString(reader.GetOrdinal("Phone")),
                       });
                   }
                  
                }
            }
            return Doctors;
        }
        #endregion
    }
}