using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class ContactController : HomeController
    {
        // GET: Admin/Contact
        DBNoiThat db = new DBNoiThat();
        public ActionResult Index()
        {
            var model = db.Contacts.ToList();
            return View(model);
        }
        public ActionResult Delete(FormCollection formCollection)
        {
            string[] ids = formCollection["ContactId"].Split(new char[] { ',' });

            foreach (string id in ids)
            {
                var model = db.Contacts.Find(Convert.ToInt32(id));
                db.Contacts.Remove(model);
                db.SaveChanges();


            }
            return RedirectToAction("Show");
        }

    }
}