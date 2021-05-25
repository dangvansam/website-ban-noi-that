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
    public class CustomerController : HomeController
    {
        DBNoiThat db = new DBNoiThat();
        // GET: Admin/Customer

        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Show()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            var user = (from a in db.Users
                          join b in db.Cards on a.UserId equals b.UserId
                          into g 
                          from d in g.DefaultIfEmpty()
                        select new UserModelView
                        {
                            UserId=a.UserId,
                            Name = a.Name,
                            Address = a.Address,
                            Phone = a.Phone,
                            Username = a.Username,
                            Password = a.Password,
                            Email = a.Email,

                            Status = a.Status,

                            GroupId = a.GroupId,
                            NumberCard = d.NumberCard,
                            Indentification=d.Identification

                        }).ToList();

            var model = user.Where(n => n.GroupId == "USER").ToList();

            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleId = "ADD_USER")]
        public ActionResult Add()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            //ViewBag.ListGroups = new SelectList(db.UserGroups.Where(a => a.GroupId != "USER").ToList(), "GroupId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserModelView n)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            var model = new User();

            model.Name = n.Name;
            model.Address = n.Address;
            model.Phone = n.Phone;
            model.Username = n.Username;
            model.Password = Encryptor.MD5Hash(n.Password);
            model.Email = n.Email;
            model.GroupId = "USER";
            model.Status = n.Status;

            db.Users.Add(model);
            db.SaveChanges();
            var models = new Card();
            models.NumberCard = 0;
            models.UserNumber = 0;
            models.UserId = n.UserId;
            models.Identification = n.Indentification;
            db.Cards.Add(models);

            db.SaveChanges();
            return RedirectToAction("Show");
        }

        [HttpGet]
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Edit(int UserId)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            //ViewBag.ListGroups = new SelectList(db.UserGroups.Where(a => a.GroupId != "USER").ToList(), "GroupId", "Name");

            var user = (from a in db.Users
                         join b in db.Cards on a.UserId equals b.UserId
                         into g
                        from d in g.DefaultIfEmpty()
                        select new UserModelView
                         {
                             UserId = a.UserId,
                             Name = a.Name,
                             Address = a.Address,
                             Phone = a.Phone,
                             Username = a.Username,
                             Password = a.Password,
                             Email = a.Email,

                             Status = a.Status,

                             GroupId = a.GroupId,
                             NumberCard=d.NumberCard,
                            Indentification = d.Identification

                        }).ToList();
            var models =user.Where(a => a.UserId == UserId).First();
            if (models == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("Show");
            }
            return View(models);
        }

        [HttpPost]
        [HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Edit(UserModelView n)
        {
            
            if (ModelState.IsValid)
            {
                //db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                var model = db.Users.SingleOrDefault(a => a.UserId == n.UserId);
               
                model.Name = n.Name;
                model.Address = n.Address;
                model.Phone = n.Phone;
                model.Username = n.Username;
                model.Password = Encryptor.MD5Hash(n.Password);
                model.Email = n.Email;
                model.GroupId = n.GroupId;

                model.Status = n.Status;
                db.SaveChanges();
                var models = db.Cards.Where(a => a.UserId == model.UserId).SingleOrDefault();
                if(models!=null)
                {
                    models.NumberCard = n.NumberCard;
                    models.Identification = n.Indentification;
                    db.SaveChanges();
                    return RedirectToAction("Show");
                }
                else
                {
                    return RedirectToAction("Show");
                }
            }
            else
            {
                return JavaScript("alert('Error');");
            }
        }

        [HttpPost]
        [HasCredential(RoleId = "DELETE_USER")]
        public ActionResult Delete(FormCollection formCollection)
        {
            string[] ids = formCollection["UserId"].Split(new char[] { ',' });

            foreach (string id in ids)
            {
                var model = db.Users.Find(Convert.ToInt32(id));
                db.Users.Remove(model);
                db.SaveChanges();
                var m = db.Cards.SingleOrDefault(n => n.UserId == model.UserId);
                if(m!=null)
                {
                    db.Cards.Remove(m);
                    db.SaveChanges();
                }
                else
                {

                }
            }
            return RedirectToAction("Show");
        }
    }
}