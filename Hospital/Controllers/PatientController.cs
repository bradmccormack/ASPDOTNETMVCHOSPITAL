using System.Web.Mvc;
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
            _vm.Search.DateOfVisit = DateTime.Today;
        }

        public PatientController(Repository Repository)
        {
            this.Repository = Repository;
        }


        public Repository Repository {get; private set;}

        [HttpGet]
        public ActionResult PatientList(string Name)
        {
            List<IPatient> Patients = null;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Name))
                    Patients = Repository.GetPatients().Where(e => e.Name.ToUpper().Contains(Name.ToUpper())).ToList<IPatient>();
                else
                    Patients = Repository.GetPatients();

            }
            else
                Patients = new List<IPatient>();
            
            return View(Patients);
        }



        [HttpGet]
        public ActionResult PatientVisits()
        {
            if(ModelState.IsValid)
                 _vm.PatientVisitations = Repository.GetVisits();
            else 
                _vm.PatientVisitations = new List<IVisit>();

            return View(_vm);
        }

        [HttpPost]
        public ActionResult PatientVisits(ViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<IVisit> Visits = Repository.GetVisits();

                if (!String.IsNullOrEmpty(vm.Search.Name))
                    Visits = Visits.Where(a => a.Name.ToUpper().Contains(vm.Search.Name.ToUpper()));

                if(vm.Search.DateOfVisit != null)
                    Visits = Visits.Where(a => a.DateOfVisit >= vm.Search.DateOfVisit);

                if (vm.Search.DateOfDischarge != null)
                    Visits = Visits.Where(a => a.DateOfDischarge <= vm.Search.DateOfDischarge);

                _vm.PatientVisitations = Visits.ToList<IVisit>();
                
            }
            else
                _vm.PatientVisitations = new List<IVisit>();

            return View(_vm);
        }


        [HttpGet]
        public ActionResult PatientEditList()
        {
            _vm.Patients = Repository.GetPatients();
            return View(_vm);
        }

        [HttpGet]
        public ActionResult PatientEdit(int PatientID)
        {
            IPatient Patient = Repository.GetPatient(PatientID);
            return View(Patient);
        }

        [HttpPost]
        public ActionResult PatientEdit(Patient Patient)
        {
            bool success = Repository.UpdatePatient(Patient);
            if(success)
                return RedirectToAction("PatientEditList");

            return View(Patient);
        }

        [HttpGet]
        public ActionResult PatientNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PatientNew(Patient Patient)
        {
            bool success = Repository.RegisterPatient(Patient);
            if (success)
                return RedirectToAction("PatientEditList");

            return View(Patient);
        }

        [HttpGet]
        public ActionResult PatientDischarge(int VisitId)
        {
           
            IVisit Visit = Repository.GetVisit(VisitId);
            IBed Bed = Repository.GetBed(Visit.BedID);
            ViewModelDischargeInvoice Model = new ViewModelDischargeInvoice();
            Model.Bed = Bed;
            Model.Visit = Visit;

            return View(Model);
        }

    }
}