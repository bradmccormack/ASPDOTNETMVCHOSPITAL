using Hospital.Models;
using System.Web.Configuration;
using System.Web.Mvc;
using Hospital.Interfaces;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        public DoctorController()
            : this(new Repository(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        {

        }

        public DoctorController(IDoctorRepository Doctors)
        {
            DoctorRepository = Doctors;
        }

        public IDoctorRepository DoctorRepository { get; private set; }

        // GET: Doctor
        public ActionResult DoctorList()
        {
            return View(DoctorRepository.GetDoctors());
        }

    }
       
}