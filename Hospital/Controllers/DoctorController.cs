using Hospital.Models;
using System.Web.Configuration;
using System.Web.Mvc;
using Hospital.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        public DoctorController()
            : this(new Repository(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        {
        }

        public DoctorController(Repository Repo)
        {
            Repository = Repo;
        }

        public Repository Repository { get; private set; }

        [HttpGet]
        public ActionResult DoctorList(string name)
        {
            List<IDoctor> Doctors = null;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(name))
                    Doctors = Repository.GetDoctors().Where(e => e.Name.ToUpper().Contains(name.ToUpper())).ToList<IDoctor>();
                else
                    Doctors = Repository.GetDoctors();
            }
            else
                Doctors = new List<IDoctor>();
         
            return View(Doctors);
        }

        [HttpGet]
        public ActionResult AssignDoctor(int DoctorID)
        {
            ViewModelDoctorAssign DoctorPatientInfo = new ViewModelDoctorAssign();
            DoctorPatientInfo.Doctor = (Doctor) Repository.GetDoctor(DoctorID);
            DoctorPatientInfo.Patients = (IEnumerable<IPatient>) Repository.GetPatients();
            DoctorPatientInfo.Beds = (IEnumerable<IBed>) Repository.GetBeds();
            DoctorPatientInfo.Visit = new Visit();
            return View(DoctorPatientInfo);
        }


        [HttpPost]
        public ActionResult AssignDoctor(Doctor Doctor, int Patients, int Beds, Visit Visit)
        {

            if(ModelState.IsValid)
            {
                bool success = Repository.RegisterVisit(Doctor.Id, Patients, Beds, Visit.DateOfVisit, Visit.Symptoms, Visit.Disease, Visit.Treatment, Visit.isInPatient);
                 
                if (success)
                    return RedirectToAction("DoctorList");
            }
            return AssignDoctor(Doctor.Id);
          
        }

    }
       
}