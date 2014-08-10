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

        public DoctorController(IDoctorRepository Doctors)
        {
            DoctorRepository = Doctors;
        }

        public IDoctorRepository DoctorRepository { get; private set; }

        [HttpGet]
        public ActionResult DoctorList(string name)
        {
            List<IDoctor> Doctors = null;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(name))
                    Doctors = DoctorRepository.GetDoctors().Where(e => e.Name.ToUpper().Contains(name.ToUpper())).ToList<IDoctor>();
                else
                    Doctors = DoctorRepository.GetDoctors();
            }
            else
                Doctors = new List<IDoctor>();
         
            return View(Doctors);
        }

    }
       
}