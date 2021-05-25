using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Common;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class UserController : HomeController
    {
        DBNoiThat db = new DBNoiThat();
        public ActionResult Index()
        {
            return View();
        }

        [HasCredential(RoleId = "VIEW_ADMIN")]
        public ActionResult Show()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            var models = db.Users.Where(n => n.GroupId != "USER").ToList();
            return View(models);
        }

        [HttpGet]
        [HasCredential(RoleId = "ADD_ADMIN")]
        public ActionResult Add()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            ViewBag.ListGroups = new SelectList(db.UserGroups.Where(a => a.GroupId != "USER").ToList(), "GroupId", "Name");
            return View();
        }

        [HttpPost]
        [HasCredential(RoleId = "ADD_ADMIN")]
        public ActionResult Add(User n)
        {
            ViewBag.ListGroups = new SelectList(db.UserGroups.Where(a => a.GroupId != "USER").ToList(), "GroupId", "Name");
            var model = new User();
            model.Name = n.Name;
            model.Address = n.Address;
            model.Phone = n.Phone;
            model.Username = n.Username;
            model.Password = Encryptor.MD5Hash(n.Password);
            model.Email = n.Email;
            model.GroupId = n.GroupId;
            model.Status = n.Status;
            db.Users.Add(model);
            db.SaveChanges();
            return RedirectToAction("Show");
        }

        [HttpGet]
        [HasCredential(RoleId = "EDIT_ADMIN")]
        public ActionResult Edit(int UserId)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            ViewBag.ListGroups = new SelectList(db.UserGroups.Where(a => a.GroupId != "USER").ToList(), "GroupId", "Name");
            var models= db.Users.Where(a => a.UserId == UserId).First();
            if (models == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("Show");
            }
            return View(models);
        }

        [HttpPost]
        [HasCredential(RoleId = "EDIT_ADMIN")]
        public ActionResult Edit(User n)
        {
            ViewBag.ListGroups = new SelectList(db.UserGroups.Where(a => a.GroupId != "USER").ToList(), "GroupId", "Name");
            if (ModelState.IsValid)
            {
                var models = db.Users.Where(a => a.UserId == n.UserId).First();
                models.Name = n.Name;
                models.Username = n.Username;
                models.Password = Encryptor.MD5Hash(n.Password);
                models.GroupId = n.GroupId;
                models.Phone = n.Phone;
                models.Status = n.Status;
                models.Email = n.Email;
                models.Address = n.Address;
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            else
            {
                return JavaScript("alert('Error');");
            }
        }


        [HttpPost]
        [HasCredential(RoleId = "DELETE_ADMIN")]
        public ActionResult Delete(FormCollection formCollection)
        {
            string[] ids = formCollection["UserId"].Split(new char[] { ',' });

            foreach (string id in ids)
            {
                var model = db.Users.Find(Convert.ToInt32(id));
                db.Users.Remove(model);
                db.SaveChanges();


            }
            return RedirectToAction("Show");
        }


        [HttpGet]
        [HasCredential(RoleId = "VIEW_ADMIN")]
        public ActionResult UserProfile()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            var model = db.Users.First(n => n.UserId == session.UserId);
            ViewBag.username = session.Username;

            return View(model);
        }

        [HttpPost]
        [HasCredential(RoleId = "EDIT_ADMIN")]
        public ActionResult UserProfile(User a)
        {
            db.Entry(a).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View();
        }

    }
}