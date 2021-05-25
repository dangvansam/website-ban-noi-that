using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.EF;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class CredentialsController : Controller
    {
        private DBNoiThat db = new DBNoiThat();

        // GET: Admin/Credentials
        public ActionResult Index()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            if (session != null)
            {
                ViewBag.username = session.Username;
            }
            //var groupid = "ADMIN";
            //UserGroup userGroup = db.UserGroups.FirstOrDefault();
            //ViewBag.groups = db.UserGroups.ToList();
            //ViewBag.group = userGroup;
            //ViewBag.rolesofgroup = db.Credentials.Where(c => c.UserGroupId == userGroup.GroupId).ToList();

            //ViewBag.ListGroups = new SelectList(db.UserGroups.Where(a => a.GroupId != "USER").ToList(), "GroupId", "Name");
            return View(db.Credentials.ToList());
        }

        // GET: Admin/Credentials/Details/5
        public ActionResult Details(int groupid)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            if (session != null)
            {
                ViewBag.username = session.Username;
            }

            if (groupid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroups.Find(groupid);
            ViewBag.group = userGroup;
            //ViewBag.rolesofgroup = db.Credentials.Where(c => c.UserGroupId == groupid).ToList();
            //Credential credential = db.Credentials.Find(id);
            
            return View(db.Credentials.SingleOrDefault(c=>c.CredenId == groupid));
        }

        // GET: Admin/Credentials/Create
        public ActionResult Create()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            if (session != null)
            {
                ViewBag.username = session.Username;
            }
            ViewBag.ListGroups = new SelectList(db.UserGroups.ToList(), "GroupId", "Name");
            ViewBag.ListRoles = new SelectList(db.Roles.ToList(), "RoleId", "Name");
            return View();
        }

        // POST: Admin/Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CredenId,UserGroupId,RoleId")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Credentials.Add(credential);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(credential);
        }

        // GET: Admin/Credentials/Edit/5
        public ActionResult Edit(int? id)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        // POST: Admin/Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CredenId,UserGroupId,RoleId")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credential).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(credential);
        }

        // GET: Admin/Credentials/Delete/5
        public ActionResult Delete(int? id)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(id);
            db.Credentials.Remove(credential);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
