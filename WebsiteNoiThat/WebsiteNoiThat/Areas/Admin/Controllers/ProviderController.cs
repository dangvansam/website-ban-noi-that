using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Common;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class ProviderController : HomeController
    {
        // GET: Admin/Provider
        DBNoiThat db = new DBNoiThat();

        [HasCredential(RoleId = "VIEW_PROVIDER")]
        public ActionResult Index()
        {
            return View();
        }

        [HasCredential(RoleId = "VIEW_PROVIDER")]
        public ActionResult Show()
        {
            return View(db.Providers.ToList());
        }

        [HttpGet]
        [HasCredential(RoleId = "ADD_PROVIDER")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleId = "ADD_PROVIDER")]
        public ActionResult Add(Provider n)
        {
            var model = db.Providers.SingleOrDefault(a => a.ProviderId == n.ProviderId);
            if (model != null)
            {
                ModelState.AddModelError("ProError", "Id already in use");
                return View();
            }
            else
            {
                db.Providers.Add(n);
                db.SaveChanges();
                return RedirectToAction("Show");
            }

        }
        [HttpGet]
        [HasCredential(RoleId = "EDIT_PROVIDER")]
        public ActionResult Edit(int ProviderId)
        {
            Provider a = db.Providers.SingleOrDefault(n => n.ProviderId == ProviderId);
            if (a == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("Show");
            }
            return View(a);

        }

        [HttpPost]
        [HasCredential(RoleId = "EDIT_PROVIDER")]
        public ActionResult Edit(Provider n)
        {
            if (ModelState.IsValid)
            {
                db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            else
            {
                return JavaScript("alert('Error');");
            }
        }


        [HttpGet]
        [HasCredential(RoleId = "DELETE_PROVIDER")]
        public ActionResult Delete(int id)
        {
            var model = db.Providers.Find(Convert.ToInt32(id));
            db.Providers.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Show");
        }

    }
}