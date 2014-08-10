using System;
using Microsoft.AspNet.Identity;
using Hospital.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        private decimal _Rateperday;
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

        public decimal RatePerDay
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
                return _BedType;
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
        private DateTime _DOB;
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

        public DateTime DateOfBirth
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

    #region Visit
    public class Visit : IVisit 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool isInPatient { get; set; }
        public string DrName { get; set; }
        public string BedName { get; set; }
        public System.DateTime DateOfVisit { get; set; }
        public System.DateTime DateOfDischarge { get; set; }
        public string Symptoms { get; set; }
        public string Disease { get; set; }
        public string Treatment { get; set; }

    }

    public class VisitSearch : IVisitSearch
    {
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Must be under 50 characters", MinimumLength = 0)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Admision date required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public System.DateTime DateOfVisit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd")]
        public System.DateTime? DateOfDischarge { get; set; }
    }

    public class ViewModel
    {
        public IEnumerable<IVisit> PatientVisitations { get; set; }
        public IEnumerable<IPatient> Patients { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public VisitSearch Search { get; set; }
    }
#endregion


    #endregion


}