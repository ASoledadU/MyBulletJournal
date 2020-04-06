using System.Linq;
using System.Web.Mvc;

namespace MyBulletJournal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (myJournalDatabaseEntities dc = new myJournalDatabaseEntities())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "“My Bullet Journal” is a web application that is dedicated to " +
                "fulfilling all your simple organization needs. With customization, you are able" +
                " to put the MY in “My Bullet Journal”! If you have any questions regarding your " +
                "journal please feel free to go to our contact page to communicate with our dedicated " +
                "team of specialists.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If for any reason you are having trouble using " +
                "your Bullet Journal please feel free to contact our helpdesk specialists.";

            return View();
        }

        public ActionResult Key()
        {
            return View();
        }
    }
}