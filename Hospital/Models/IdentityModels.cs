using System;
using Microsoft.AspNet.Identity;
using Hospital.Interfaces;

namespace Hospital.Models
{
    #region POCO 
    public class User : IUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string Id
        {
            get { return UserId.ToString(); }
        }
    }

    public class Bed : IBed
    {
        private int _Id;
        private string _Name;
        private float _Rateperday;
        private string _BedType;


        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public float RatePerDay
        {
            get
            {
                return  _Rateperday;
            }
            set
            {
                _Rateperday = value;
            }
        }

        public string BedType
        {
            get
            {
                return BedType;
            }
            set
            {
                _BedType = value;
            }
        }
    }

    public class Doctor : IDoctor
    {
        private int _Id;
        private String _Name;
        private String _Address;
        private String _Phone;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }

        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }
    }

    public class Patient : IPatient
    {
        private int _Id;
        private String _Name;
        private String _Address;
        private String _DOB;
        private String _Phone;
        private String _Emergency;
        private DateTime _DOR;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }

        public string DateOfBirth
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
            }
        }

        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }

        public string EmergencyContact
        {
            get
            {
                return _Emergency;
            }
            set
            {
                _Emergency = value;
            }
        }

        public DateTime DateOfRegistration
        {
            get
            {
                return _DOR;
            }
            set
            {
                _DOR = value;
            }
        }
    }

    public class Visit : IVisit 
    {
        private int _Id;
        private bool _PatientType;
        private int _DoctorId;
        private int _PatientID;
        private int _BedID;
        private DateTime _DateOfVisit;
        private DateTime _DateOfDischarge;
        private String _Symptoms;
        private String _Disease;
        private String _Treatment;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public bool PatientType
        {
            get
            {
                return _PatientType;
            }
            set
            {
                _PatientType = value;
            }
        }

        public int DoctorID
        {
            get
            {
                return _DoctorId;
            }
            set
            {
                _DoctorId = value;
            }
        }

        public int PatientID
        {
            get
            {
                return _PatientID;
            }
            set
            {
                _PatientID = value;
            }
        }

        public int BedID
        {
            get
            {
                return _BedID;
            }
            set
            {
                _BedID = value;
            }
        }

        public DateTime DateOfVisit
        {
            get
            {
                return _DateOfVisit;
            }
            set
            {
                _DateOfVisit = value;
            }
        }

        public DateTime DateOfDischarge
        {
            get
            {
                return _DateOfDischarge;
            }
            set
            {
                _DateOfDischarge = value;
            }
        }

        public string Symptoms
        {
            get
            {
                return _Symptoms;
            }
            set
            {
                _Symptoms = value;
            }
        }

        public string Disease
        {
            get
            {
                return _Disease;
            }
            set
            {
                _Disease = value;
            }
        }

        public string Treatment
        {
            get
            {
                return _Treatment;
            }
            set
            {
                _Treatment = value;
            }
        }
    }

    #endregion


}