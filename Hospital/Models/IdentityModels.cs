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

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal RatePerDay { get; set; }
        public string BedType { get; set; }
    }

    public class Doctor : IDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class Patient : IPatient
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public String Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "Must be under 255 characters")]
        public String Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Must be under 20 characters")]
        public string Phone { get; set; }
         
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "Must be under 20 characters")]
        public string EmergencyContact { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfRegistration { get; set; }
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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public System.DateTime? DateOfVisit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd")]
        public System.DateTime? DateOfDischarge { get; set; }
    }

    public class ViewModel
    {
        public IEnumerable<IVisit> PatientVisitations { get; set; }
        public IEnumerable<IPatient> Patients { get; set; }
        public VisitSearch Search { get; set; }
    }
#endregion


    #endregion


}