using System;
using System.IO;
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
        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (myJournalDatabaseEntities dc = new myJournalDatabaseEntities())
            {
                if (e.EventId > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (myJournalDatabaseEntities dc = new myJournalDatabaseEntities())
            {
                var v = dc.Events.Where(a => a.EventId == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        myJournalDatabaseEntities db = new myJournalDatabaseEntities();
        public ActionResult AddNewEntry()
        {
            return View();
        }
        /*
        public ActionResult SaveData(JournalE entry)
        {
          if (entry.JournalTitle != null && entry.UploadImage != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(entry.UploadImage.FileName);
                string extension = Path.GetExtension(entry.UploadImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                entry.PicUrl = fileName;
                entry.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
                db.JournalEs.Add(entry);
                db.SaveChanges();
            }
            var result = "Successfully Added";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        */
        public ActionResult About()
        {
            ViewBag.Message = "“My Bullet Journal” is a web application that is dedicated to " +
                "fulfilling your simple organization needs. With customization, you are able" +
                " to put the MY in “My Bullet Journal”!";

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

        public ActionResult Calendar()
        {
            return View();
        }
    }
}