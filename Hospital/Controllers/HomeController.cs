using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace Hospital.Controllers
{

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        protected string GetUrl(object routeValues)
        {
            RouteValueDictionary values = new RouteValueDictionary(routeValues);
            RequestContext context = new RequestContext(HttpContext, RouteData);

            string url = RouteTable.Routes.GetVirtualPath(context, values).VirtualPath;

            return new Uri(Request.Url, url).AbsoluteUri;
        }

        [HttpGet]
        //http://rbonini.wordpress.com/2011/04/08/sitemaps-in-asp-net-mvc-icing-on-the-cake/ Attribution
        public ContentResult Sitemap()
        {
            
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            List<string> urlList = new List<string>();
            urlList.Add(GetUrl(new { controller = "Home", action = "Index" }));
            urlList.Add(GetUrl(new { controller = "Home", action = "Sitemap" }));
            urlList.Add(GetUrl(new { controller = "Account", action = "Login" }));
            urlList.Add(GetUrl(new { controller = "Account", action = "ExternalLogin" }));
            urlList.Add(GetUrl(new { controller = "Bed", action = "BedList" }));
            urlList.Add(GetUrl(new { controller = "Doctor", action = "DoctorList" }));
            urlList.Add(GetUrl(new { controller = "Error", action = "Error404" }));
            urlList.Add(GetUrl(new { controller = "Patient", action = "PatientList" }));
            urlList.Add(GetUrl(new { controller = "Patient", action = "PatientVisits" }));
            urlList.Add(GetUrl(new { controller = "Patient", action = "PatientEditList" }));
            urlList.Add(GetUrl(new { controller = "PatientEdit", action = "PatientEdit" }));
            urlList.Add(GetUrl(new { controller = "PatientNew", action = "PatientNew" }));

            foreach (var item in urlList)
            {
                root.Add(
                new XElement("url",
                new XElement("loc", item),
                new XElement("changefreq", "daily")));
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms, Encoding.UTF8))
                {
                    root.Save(writer);
                }
                return Content(Encoding.UTF8.GetString(ms.ToArray()), "text/xml", Encoding.UTF8);
            }
        }
    }
    
}