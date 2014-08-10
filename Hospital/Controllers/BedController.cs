using Hospital.Models;
using System.Web.Configuration;
using System.Web.Mvc;
using Hospital.Interfaces;
using System.Collections.Generic;

namespace Hospital.Controllers
{
    public class BedController : Controller
    {
        public BedController()
            : this(new Repository(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        {

        }

        public BedController(IBedRepository Beds)
        {
            BedRepository = Beds;
        }

        public IBedRepository BedRepository { get; private set; }
        
        [HttpGet]
        public ActionResult BedList()
        {
            List<IBed> Beds;
            if (ModelState.IsValid)
                Beds = BedRepository.GetBeds();
            else
                Beds = new List<IBed>();

            return View(Beds);
        }

    }

}