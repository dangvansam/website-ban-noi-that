using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using System.Web.Script.Serialization;
using PagedList;
using PagedList.Mvc;
using System.IO;
using WebsiteNoiThat.Common;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class NewssController : HomeController
    {
        // GET: Admin/News
        DBNoiThat db = new DBNoiThat();

        [HasCredential(RoleId = "VIEW_NEWS")]
        public ActionResult Index()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            return View();
        }

        [HasCredential(RoleId = "VIEW_NEWS")]
        public ActionResult Show()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            var model = db.News.ToList();
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleId = "ADD_NEWS")]
        public ActionResult Add()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            return View();
        }

        [HttpPost]
        [HasCredential(RoleId = "ADD_NEWS")]
        public ActionResult Add(News n, HttpPostedFileBase file)
        {
            var model = db.News.SingleOrDefault(a => a.NewsId == n.NewsId);

            if (model != null)
            {
                ModelState.AddModelError("NewError", "Id already in user");

                return View();
            }
            else
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/image"), fileName);
                file.SaveAs(path);
                n.Photo = file.FileName;
                db.News.Add(n);
                db.SaveChanges();
                return RedirectToAction("Show");
            }
        }

        [HttpGet]
        [HasCredential(RoleId = "EDIT_NEWS")]
        public ActionResult Edit(int NewsId)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            News a = db.News.SingleOrDefault(n => n.NewsId == NewsId);
            if (a == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("Show");
            }
            return View(a);

        }

        [HttpPost, ValidateInput(false)]
        [HasCredential(RoleId = "EDIT_NEWS")]
        public ActionResult Edit(News n, HttpPostedFileBase UploadImage)
        {

            if (ModelState.IsValid)
            {
                NewsDao a = new NewsDao();
                if (UploadImage != null)
                {
                    // Delete exiting file
                    //System.IO.File.Delete(Path.Combine(Server.MapPath("~/image"),n.Photo));
                    // Save new file
                    string fileName = /*Guid.NewGuid() +*/ Path.GetFileName(UploadImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/image"), fileName);
                    UploadImage.SaveAs(path);
                    n.Photo = fileName;
                }
                if (a.Update(n))
                {

                    return RedirectToAction("Show");
                }
                else
                {
                    return JavaScript("alert('didn't add');");
                }

            }
            else
            {
                return JavaScript("alert('Error');");
            }
        }

        [HttpGet]
        [HasCredential(RoleId = "DELETE_NEWS")]
        public ActionResult Delete()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            return View();
        }

        [HttpPost]
        [HasCredential(RoleId = "DELETE_NEWS")]
        public ActionResult Delete(FormCollection formCollection)
        {
            string[] ids = formCollection["NewsId"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var model = db.News.Find(Convert.ToInt32(id));
                db.News.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("Show");
        }

    }
}