using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Models;
using Rotativa;
using WebsiteNoiThat.Common;
using PagedList;
using System.Net;
using WebsiteNoiThat.Areas.Admin.Models;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class OrderController : HomeController
    {

        DBNoiThat db = new DBNoiThat();

        [HttpGet]
        [HasCredential(RoleId = "VIEW_ORDER")]
        public ActionResult Show()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            List<Status> list = db.Status.ToList();
            ViewBag.StatusList = new SelectList(list, "StatusId", "Name");

            var model = (from a in db.Orders
                         join b in db.OrderDetails
                          on a.OrderId equals b.OrderId
                         join c in db.Products
                         on b.ProductId equals c.ProductId
                         join d in db.Status on a.StatusId equals d.StatusId
                         select new OrderViewModel
                         {
                             OrderDetailId1 = b.OrderDetailId,
                             OrderId = a.OrderId,
                             ProductId = b.ProductId,
                             ShipAddress = a.ShipAddress,
                             ShipName = a.ShipName,
                             ShipPhone = a.ShipPhone,
                             Price = b.Price,
                             Quantity = b.Quantity,
                             Discount = c.Discount,
                             UpdateDate = a.UpdateDate,
                             StatusId = a.StatusId,
                             StatusName = d.Name,
                             UserId = a.UserId
                         }).ToList();

            ViewBag.Status = db.Status.ToList();
            return View(model);
        }

        [HttpPost]
        [HasCredential(RoleId = "VIEW_ORDER")]
        public ActionResult Show(OrderViewModel model)
        {
            try
            {
                List<Status> list = db.Status.ToList();
                ViewBag.StatusList = new SelectList(list, "StatusId", "Name");

                if (model.OrderDetailId1 > 0)
                {
                    //update
                    OrderDetail emp = db.OrderDetails.SingleOrDefault(x => x.OrderDetailId == model.OrderDetailId1);
                    //emp.StatusId = model.StatusId;
                    db.SaveChanges();
                }
                return Redirect("~/Admin/Order/Show");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HasCredential(RoleId = "EDIT_ORDER")]
        public ActionResult AddEditOrder(int OrderDetailId)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            List<Status> list = db.Status.ToList();
            ViewBag.StatusList = new SelectList(list, "StatusId", "Name");
            OrderViewModel model = new OrderViewModel();

            if (OrderDetailId > 0)
            {

                OrderDetail emp = db.OrderDetails.SingleOrDefault(x => x.OrderDetailId == OrderDetailId);
                //model.StatusId = emp.StatusId;
                model.OrderId = emp.OrderId;
                model.ProductId = emp.ProductId;
                model.Price = emp.Price;
                model.Quantity = emp.Quantity;
                model.OrderDetailId1 = emp.OrderDetailId;

            }
            return PartialView("~/Areas/Admin/Views/Order/Partial2.cshtml", model);
        }


        [HttpPost]
        [HasCredential(RoleId = "DELETE_ORDER")]
        public ActionResult Delete(FormCollection formCollection)
        {
            string[] ids = formCollection["OrderDetailId1"].Split(new char[] { ',' });

            foreach (string id in ids)
            {
                var model = db.OrderDetails.Find(Convert.ToInt32(id));
                db.OrderDetails.Remove(model);
                db.SaveChanges();


            }
            return RedirectToAction("Show");
        }

        [HasCredential(RoleId = "VIEW_ORDER")]
        public ActionResult Index()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            var models = (from a in db.OrderDetails
                          join b in db.Orders
                          on a.OrderId equals b.OrderId
                          join c in db.Products
                          on a.ProductId equals c.ProductId
                          join d in db.Status on b.StatusId equals d.StatusId
                          select new OrderViewModel
                          {
                              OrderDetailId1 = a.OrderDetailId,
                              OrderId = a.OrderId,
                              ProductId = a.ProductId,
                              Price = a.Price,
                              Quantity = a.Quantity,
                              Discount = c.Discount,
                              UpdateDate = b.UpdateDate,

                          }).ToList();
            var total = 0;
            foreach (OrderViewModel item in models)
            {
                total = total + Convert.ToInt32((item.Quantity * item.Price));

            }
            ViewBag.total = total;
            return View(models);
        }

        [HttpGet]
        [HasCredential(RoleId = "VIEW_ORDER")]
        public ActionResult Viewmodel()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;
            var total = 0;
            var model = (from a in db.Orders
                         join b in db.OrderDetails
                          on a.OrderId equals b.OrderId
                         join c in db.Products
                         on b.ProductId equals c.ProductId
                         join d in db.Status on a.StatusId equals d.StatusId
                         select new OrderViewModel
                         {
                             OrderDetailId1 = b.OrderDetailId,
                             OrderId = a.OrderId,
                             ProductId = b.ProductId,
                             ShipAddress = a.ShipAddress,
                             ShipName = a.ShipName,
                             ShipPhone = a.ShipPhone,
                             Price = b.Price,
                             Quantity = b.Quantity,
                             Discount = c.Discount,
                             UpdateDate = a.UpdateDate,
                             StatusId = a.StatusId,
                             StatusName = d.Name,
                             UserId = a.UserId
                         }).ToList();
            foreach (OrderViewModel item in model)
            {
                total = total + Convert.ToInt32((item.Quantity * item.Price));
            }
            ViewBag.total = total;

            return View(model);
        }

        [HttpPost]
        public ActionResult Viewmodel(DateTime dfr, DateTime dto)
        {
            var models = (from a in db.Orders
                         join b in db.OrderDetails
                          on a.OrderId equals b.OrderId
                         join c in db.Products
                         on b.ProductId equals c.ProductId
                         join d in db.Status on a.StatusId equals d.StatusId
                         select new OrderViewModel
                         {
                             OrderDetailId1 = b.OrderDetailId,
                             OrderId = a.OrderId,
                             ProductId = b.ProductId,
                             ShipAddress = a.ShipAddress,
                             ShipName = a.ShipName,
                             ShipPhone = a.ShipPhone,
                             Price = b.Price,
                             Quantity = b.Quantity,
                             Discount = c.Discount,
                             UpdateDate = a.UpdateDate,
                             StatusId = a.StatusId,
                             StatusName = d.Name,
                             UserId = a.UserId

                         }).ToList();

            var model = models.Where(n => n.UpdateDate >= dfr && n.UpdateDate <= dto).ToList();
            var total = 0;
            foreach (OrderViewModel item in model)
            {
                total = total + Convert.ToInt32((item.Quantity * item.Price));
            }
            ViewBag.total = total;
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
                         ).Where(o => o.OrderId == order.OrderId).ToList();
                ViewBag.orderproducts = orderproducts;

                double? total = 0;
                foreach (OrderProduct item in orderproducts)
                {
                    total += item.Price;
                }
                ViewBag.total = total;

                return View(order);
            }
        }

        [HttpDelete]
        [HasCredential(RoleId = "DELETE_ORDER")]
        public ActionResult Delete(int id)

        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            var model = db.OrderDetails.SingleOrDefault(n => n.OrderDetailId == id);
            db.OrderDetails.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Show");
        }


        [HttpGet]
        [HasCredential(RoleId = "EDIT_ORDER")]
        public ActionResult Edit(int OrderDetailId)
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            ViewBag.username = session.Username;

            List<Status> list = db.Status.ToList();
            ViewBag.StatusList = new SelectList(list, "StatusId", "Name");
            OrderViewModel model = new OrderViewModel();

            if (OrderDetailId > 0)
            {

                OrderDetail emp = db.OrderDetails.SingleOrDefault(x => x.OrderDetailId == OrderDetailId);
                //model.StatusId = emp.StatusId;
                model.OrderId = emp.OrderId;
                model.ProductId = emp.ProductId;
                model.Price = emp.Price;
                model.Quantity = emp.Quantity;
                model.OrderDetailId1 = emp.OrderDetailId;

            }

            return View(model);
        }

        [HttpPost]
        [HasCredential(RoleId = "EDIT_ORDER")]
        public ActionResult Edit(OrderViewModel model)
        {
            //List<Status> list = db.Status.ToList();
            //ViewBag.StatusList = new SelectList(list, "StatusId", "Name");

            if (model.OrderDetailId1 > 0)
            {
                //update
                Order oder = db.Orders.SingleOrDefault(x => x.OrderId == model.OrderId);
                oder.StatusId = model.StatusId.GetValueOrDefault();
                db.SaveChanges();
            }
            return Redirect("~/Admin/Order/Show");
        }

        public ActionResult IndexById(int id)
        {
            //var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            //ViewBag.username = session.Username;
            Order order = db.Orders.Find(id);
            ViewBag.aaaa = db.Status.SingleOrDefault(x => x.StatusId == order.StatusId).Name;
            if (order == null)
            {
                return View("Show");
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
                         ).Where(o => o.OrderId == order.OrderId).ToList();
                ViewBag.orderproducts = orderproducts;

                double? total = 0;
                foreach (OrderProduct item in orderproducts)
                {
                    total += item.Price;
                }
                ViewBag.total = total;

                return View(order);

                //var model = (from a in db.OrderDetails
                //             join b in db.Orders on a.OrderId equals b.OrderId
                //             join c in db.Products on a.ProductId equals c.ProductId
                //             where b.UserId == id
                //             select new OrderViewModel
                //             {
                //                 OrderDetailId1 = a.OrderDetailId,
                //                 OrderId = a.OrderId,
                //                 ProductId = a.ProductId,
                //                 ShipAddress = b.ShipAddress,
                //                 ShipName = b.ShipName,
                //                 ShipPhone = b.ShipPhone,
                //                 Price = a.Price,
                //                 Quantity = a.Quantity,
                //                 Discount = c.Discount,
                //                 UpdateDate = b.UpdateDate,
                //                 StatusId = b.StatusId

                //             }).ToList();
                //if (model != null)
                //{
                //    double? total = 0;
                //    foreach (var item in model)
                //    {
                //        total += ((item.Price - (item.Price * item.Discount * 0.01)) * (item.Quantity));
                //        ViewBag.Total = total;
                //    }

                //    return View(model);
                //}
                //    else
                //{
                //    return View("Show");
                //}

            }
        }

        public ActionResult PrintSalarySlip(int id)
        {
            //var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            //ViewBag.username = session.Username;

            var report = new ActionAsPdf("IndexById", new { id = id });
            return report;
        }



    }
}