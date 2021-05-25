using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using WebsiteNoiThat.Areas.Admin.Models;
using WebsiteNoiThat.Common;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class OrdersController : HomeController
    {
        private DBNoiThat db = new DBNoiThat();

        [HasCredential(RoleId = "VIEW_ORDER")]
        public ActionResult Show()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            var model = (from a in db.Orders
                         join b in db.Status
                         on a.StatusId equals b.StatusId
                         select new OrderView
                         {
                             OrderId = a.OrderId,
                             ShipAddress = a.ShipAddress,
                             ShipEmail = a.ShipEmail,
                             ShipName = a.ShipName,
                             ShipPhone = a.ShipPhone,
                             StatusName = b.Name,
                             UpdateDate = a.UpdateDate,
                             UserId = a.UserId,

                         }
                         ).ToList();
            return View(model);
        }

        [HasCredential(RoleId = "VIEW_ORDER")]
        public ActionResult Details(int? id)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            ViewBag.aaaa = db.Status.SingleOrDefault(x => x.StatusId == order.StatusId).Name;
            if (order == null)
            {
                return HttpNotFound();
            }
            else
            {
                var orderproducts = (
                                 from a in db.OrderDetails
                                 join b in db.Orders
                                 on a.OrderId equals b.OrderId
                                 join c in db.Products
                                 on a.ProductId equals c.ProductId
                                 select new OrderProduct
                                 {
                                     OrderId = a.OrderId,
                                     ProductName = c.Name,
                                     Quantity = a.Quantity,
                                     Price = a.Price,
                                     ProductId = c.ProductId
                                 }
                         ).Where(o=>o.OrderId==order.OrderId).ToList();
                ViewBag.orderproducts = orderproducts;

                double? total = 0;
                foreach(OrderProduct item in orderproducts)
                {
                    total += item.Price;
                }
                ViewBag.total = total;

                return View(order);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,ShipName,UserId,ShipPhone,ShipEmail,UpdateDate,ShipAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HasCredential(RoleId = "EDIT_ORDER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.ListStatus = new SelectList(db.Status.ToList(), "StatusId", "Name");
                return View(order);
            }
        }

        [HasCredential(RoleId = "EDIT_ORDER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                order.UpdateDate = DateTime.Now;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            return View(order);
        }

        [HasCredential(RoleId = "DELETE_ORDER")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HasCredential(RoleId = "DELETE_ORDER")]

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Show");
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
