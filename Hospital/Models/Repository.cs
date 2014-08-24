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
        public IBed GetBed(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using(var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT BedNAme, RatePerDay, BedType , id FROM Bed WHERE Id = @id";
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

        public List<IBed> GetBeds()
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
        public IPatient GetPatient(int id)
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


        public bool RegisterPatient(IPatient patient)
        {
            bool success = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Patient(Name, Address, DateOfBirth, Phone, EmergencyContact, DateOfRegistration) VALUES (@Name, @Address, @DOB, @Phone, @Emergency, @DOR)";
                cmd.Parameters.AddWithValue("@Id", patient.Id);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Address", patient.Address);
                cmd.Parameters.AddWithValue("@DOB", patient.DateOfBirth);
                cmd.Parameters.AddWithValue("@Emergency", patient.EmergencyContact);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@DOR", patient.DateOfRegistration);

                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    success = rows > 0;
                }
                catch
                {
                    success = false;
                }
                return success;

            }
        }

        public bool UpdatePatient(IPatient patient)
        {
            bool success = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE Patient SET Name = @Name, Address = @Address, DateOfBirth = @DOB, Phone = @Phone, EmergencyContact = @Emergency, DateOfRegistration = @DOR WHERE ID = @Id";
                cmd.Parameters.AddWithValue("@Id", patient.Id);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Address", patient.Address);
                cmd.Parameters.AddWithValue("@DOB", patient.DateOfBirth);
                cmd.Parameters.AddWithValue("@Emergency", patient.EmergencyContact);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@DOR", patient.DateOfRegistration);

                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    success = rows > 0;
                }
                catch
                {
                    success = false;
                }
                return success;

            }
        }


        public List<IPatient> GetPatients()
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

        public IVisit GetVisit(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"SELECT V.ID, P.Name AS PatientName, P.Address, V.PatientType AS InPatient, D.Name AS DocName, B.BedName,
                                    V.DateofVisit, V.DateofDischarge, V.Symptoms, V.Disease, V.Treatment, B.ID as BedID
                                    FROM Visit V
                                    JOIN Patient P ON P.ID = V.PatientID
                                    JOIN Doctor D ON D.ID = V.DoctorID
                                    JOIN Bed B ON B.ID = V.BedID
                                    WHERE V.ID = @id 
                                    ORDER BY DateOfVisit DESC ";
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    DateTime? Discharge;
                    if(reader.IsDBNull(reader.GetOrdinal("DateofDischarge")))
                        Discharge = null;
                    else Discharge = reader.GetDateTime(reader.GetOrdinal("DateofDischarge"));
                    
                    return new Visit
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        BedID = reader.GetInt32(reader.GetOrdinal("BedID")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        isInPatient = reader.GetBoolean(reader.GetOrdinal("InPatient")),
                        Name = reader.GetString(reader.GetOrdinal("PatientName")),
                        DrName = reader.GetString(reader.GetOrdinal("DocName")),
                        BedName = reader.GetString(reader.GetOrdinal("BedName")),
                        DateOfVisit = reader.GetDateTime(reader.GetOrdinal("DateOfVisit")),
                        DateOfDischarge = Discharge,
                        Symptoms = reader.GetString(reader.GetOrdinal("Symptoms")),
                        Disease = reader.GetString(reader.GetOrdinal("Disease")),
                        Treatment = reader.GetString(reader.GetOrdinal("Treatment"))
                    };
                }
            }
        }

   

        public List<IVisit> GetDoctorVisits(int DoctorID)
        {
            List<IVisit> DoctorVisits = new List<IVisit>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"SELECT V.ID, P.Name AS PatientName, P.Address, V.PatientType AS InPatient,
                                    D.Name AS DocName, B.BedName AS BedName, V.DateofVisit, V.DateofDischarge, 
                                    V.Symptoms, V.Disease, V.Treatment,  B.ID as BedID
                                    FROM Visit V
                                    JOIN Patient P ON P.ID = V.PatientID
                                    JOIN Doctor D ON D.ID = V.DoctorID
                                    JOIN Bed B ON B.ID = V.BedID
                                    Where D.ID = @DoctorID
                                    ORDER BY DateofVisit, PatientName";

                cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        DateTime? Discharge;
                        if (reader.IsDBNull(reader.GetOrdinal("DateofDischarge")))
                            Discharge = null;
                        else Discharge = reader.GetDateTime(reader.GetOrdinal("DateofDischarge"));

                        DoctorVisits.Add(new Visit
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            BedID = reader.GetInt32(reader.GetOrdinal("BedID")),
                            Name = reader.GetString(reader.GetOrdinal("PatientName")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            isInPatient = reader.GetBoolean(reader.GetOrdinal("InPatient")),
                            DrName = reader.GetString(reader.GetOrdinal("DocName")),
                            BedName = reader.GetString(reader.GetOrdinal("BedName")),
                            DateOfVisit = reader.GetDateTime(reader.GetOrdinal("DateOfVisit")),
                            DateOfDischarge = Discharge,
                            Symptoms = reader.GetString(reader.GetOrdinal("Symptoms")),
                            Disease = reader.GetString(reader.GetOrdinal("Disease")),
                            Treatment = reader.GetString(reader.GetOrdinal("Treatment"))
                        });
                    }
                }
            }
            return DoctorVisits;
        }

        public List<IVisit> GetVisits()
        {
            List<IVisit> PatientVisits = new List<IVisit>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"SELECT V.ID, P.Name AS PatientName, P.Address, V.PatientType AS InPatient, D.Name AS DocName, B.BedName, 
                                    V.DateofVisit, V.DateofDischarge, V.Symptoms, V.Disease, V.Treatment , B.ID as BedID
                                    FROM Visit V
                                    JOIN Patient P ON P.ID = V.PatientID
                                    JOIN Doctor D ON D.ID = V.DoctorID
                                    JOIN Bed B ON B.ID = V.BedID
                                    ORDER BY DateofVisit, PatientName";
                                  

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        DateTime? Discharge;
                        if (reader.IsDBNull(reader.GetOrdinal("DateofDischarge")))
                            Discharge = null;
                        else Discharge = reader.GetDateTime(reader.GetOrdinal("DateofDischarge"));
                    

                        PatientVisits.Add(new Visit
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            BedID = reader.GetInt32(reader.GetOrdinal("BedID")),
                            Name = reader.GetString(reader.GetOrdinal("PatientName")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            isInPatient = reader.GetBoolean(reader.GetOrdinal("InPatient")),
                            DrName = reader.GetString(reader.GetOrdinal("DocName")),
                            BedName = reader.GetString(reader.GetOrdinal("BedName")),
                            DateOfVisit = reader.GetDateTime(reader.GetOrdinal("DateOfVisit")),
                            DateOfDischarge = Discharge,
                            Symptoms = reader.GetString(reader.GetOrdinal("Symptoms")),
                            Disease = reader.GetString(reader.GetOrdinal("Disease")),
                            Treatment = reader.GetString(reader.GetOrdinal("Treatment"))
                        });
                    }
                }
            }
            return PatientVisits;
        }

        public bool RegisterVisit(int DoctorID, int PatientID, int BedID, DateTime AdmissionDate, string Symptoms, string Disease, string Treatment, bool InPatient)
        {
            bool success = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Visit(PatientType, DoctorID, PatientID, BedID, DateofVisit,Symptoms, Disease, Treatment) Values(@PatientType, @DoctorID, @PatientID, @BedID, @DateOfVisit, @Symptoms, @Disease, @Treatment)";
                cmd.Parameters.AddWithValue("@PatientType", InPatient);
                cmd.Parameters.AddWithValue("@PatientID", PatientID);
                cmd.Parameters.AddWithValue("@DoctorID", DoctorID);
                cmd.Parameters.AddWithValue("@BedID", BedID);
                cmd.Parameters.AddWithValue("@DateOfVisit", AdmissionDate);
               // cmd.Parameters.AddWithValue("@DateOfDischarge", DateTime.MinValue);
                cmd.Parameters.AddWithValue("@Symptoms", Symptoms);
                cmd.Parameters.AddWithValue("@Disease", Disease);
                cmd.Parameters.AddWithValue("@Treatment", Treatment);
          
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    success = rows > 0;
                }
                catch
                {
                    success = false;
                }
                return success;

            }
        }

        #endregion

        #region Doctor
        public IDoctor GetDoctor(int id)
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

        public List<IDoctor> GetDoctors()
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