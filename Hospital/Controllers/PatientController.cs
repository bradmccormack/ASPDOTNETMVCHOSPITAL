using System.Web.Mvc;
using Hospital.Interfaces;
using Hospital.Models;
using System.Web.Configuration;

namespace Hospital.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
  
        public PatientController()
            : this(new Repository(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        {

        }

        public PatientController(IPatientRepository Patients)
        {
            PatientRepository = Patients;
        }

        public IPatientRepository PatientRepository {get; private set;}

        public ActionResult PatientList()
        {
            return View(PatientRepository.GetPatients());
        }

        public ActionResult PatientVisits()
        {
            return View(PatientRepository.GetVisits());
        }
    }
}