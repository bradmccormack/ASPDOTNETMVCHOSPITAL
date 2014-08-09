using Hospital.Models;
using System.Web.Configuration;
using System.Web.Mvc;
using Hospital.Interfaces;

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

        // GET: Bed
        public ActionResult BedList()
        {
            return View(BedRepository.GetBeds());
        }

    }

}