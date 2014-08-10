﻿using System.Web.Mvc;
using Hospital.Interfaces;
using Hospital.Models;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Hospital.Controllers
{



    [Authorize]
    public class PatientController : Controller
    {
        private ViewModel _vm;

        public ViewModel ViewModel { get; private set; }

        public PatientController()
            : this(new Repository(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        {
            _vm = new ViewModel();
            _vm.Search = new VisitSearch();
        }

        public PatientController(IPatientRepository Patients)
        {
            PatientRepository = Patients;
        }

        public IPatientRepository PatientRepository {get; private set;}

        public ActionResult PatientList(string Name)
        {
            List<IPatient> Patients = null;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Name))
                    Patients = PatientRepository.GetPatients().Where(e => e.Name.ToUpper().Contains(Name.ToUpper())).ToList<IPatient>();
                else
                    Patients = PatientRepository.GetPatients();

            }
            else
                Patients = new List<IPatient>();
            
            return View(Patients);
        }

        public ActionResult PatientVisits(ViewModel vm)
        {
            if(ModelState.IsValid)
            {
                if (vm.Search == null)
                    _vm.PatientVisitations = PatientRepository.GetVisits();
                else
                {
                    
                    IEnumerable<IVisit> Visits = PatientRepository.GetVisits();

                    if (!String.IsNullOrEmpty(vm.Search.Name))
                        Visits = Visits.Where(a => a.Name.ToUpper().Contains(vm.Search.Name.ToUpper()));

                    //THE DATE IS ALWAYS COMING BACK AS MIN WTF FUCK 
                    Visits = Visits.Where(a => a.DateOfVisit >= vm.Search.DateOfVisit);

                    if (vm.Search.DateOfDischarge != null)
                        Visits = Visits.Where(a => a.DateOfDischarge <= vm.Search.DateOfDischarge);

                    _vm.PatientVisitations = Visits.ToList<IVisit>();
                }
            }
            else 
                _vm.PatientVisitations = new List<IVisit>();

            return View(_vm);

           
        }

    }
}