using BotDetect.Web.Mvc;
using Models.DAO;
using Models.EF;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Areas.Admin.Models;
using WebsiteNoiThat.Common;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.Controllers
{
    public class RegisterAndLoginController : Controller
    {
        // GET: RegisterAndLogin
        DBNoiThat db = new DBNoiThat();

        public ActionResult Logout()
        {
            Session[Commoncontent.user_sesion] = null;
            Session[Commoncontent.CartSession] = null;
            return Redirect("/");
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //var result = dao.Login(model.UserName, model.Password);
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));

                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.Username = user.Username;
                    userSession.UserId = user.UserId;
                    Session.Add(Commoncontent.user_sesion, userSession);
                    return Redirect("/");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản đang bị khóa, liên hệ admin");

                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View(model);
        }
        [HttpGet]
        
        public ActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                //else if (dao.CheckEmail(model.Email))
                //{
                //    ModelState.AddModelError("", "Email đã tồn tại");
                //}
                else
                {
                    var user = new User();
                    user.Username = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.Name = model.Name;
                    user.GroupId = "USER";

                    user.Status = true;

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        var models = db.Users.SingleOrDefault(n => n.Username == model.UserName);
                        return RedirectToAction("Card", new { UserId= models.UserId });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            model = new RegisterModel();
            return View();
        }

        [HttpGet]
        public ActionResult ViewCurentUser()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion];
            if (session != null)
            {
                var model = db.Users.SingleOrDefault(n => n.UserId == session.UserId);
                return View(model);
            }
            else
            {
                return Redirect("/RegisterAndLogin/Login");
            }
        }

        [HttpGet]
        public ActionResult EditCurentUser()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion];
            var model = db.Users.SingleOrDefault(n => n.UserId == session.UserId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCurentUser([Bind(Include = "UserId,Name,Address,Phone,Username,Password,Email,GroupId,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Encryptor.MD5Hash(user.Password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewCurentUser");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Card(int UserId)

        {
           
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion];
            if(session!=null)
            {
                var checkuser = db.Cards.SingleOrDefault(n => n.UserId == session.UserId);
                if (checkuser == null)
                {
                    var m = db.Users.SingleOrDefault(n => n.UserId == UserId);
                    if (m != null)
                    {
                        var model = new Card();
                        model.UserId = session.UserId;
                        model.NumberCard = 0;
                        model.UserNumber = 0;
                        return View(model);

                    }
                    else
                    {
                        //var model = new Card();
                        //model.Username = session.Username;
                        //model.NumberCard = 0;
                        //model.UseNumber = 0;
                        var model = new Card();
                        model.UserId = session.UserId;
                        model.NumberCard = 0;
                        model.UserNumber = 0;
                        
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Đã có thẻ tích điểm. Bạn không thể đăng ký thêm.");
                    return View();
                }
            }
            else
            {
                var model = new Card();
                model.UserId = UserId;
                model.NumberCard = 0;
                model.UserNumber = 0;
                return View(model);
            }
           
           
        }
        [HttpPost]
        public ActionResult Card(Card n)
        {
            var model =new Card();
            model.UserId = n.UserId;
            model.NumberCard = 0;
            model.UserNumber = 0;
            model.Identification = n.Identification;

            db.Cards.Add(model);
            db.SaveChanges();
            ViewBag.Success = "Đăng ký thẻ thành công";
            return Redirect("/");
        }
        
        public ActionResult ViewLogin()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion];
           if(session!=null)
            {
                var model = db.Cards.SingleOrDefault(n => n.UserId == session.UserId);
                var models = (from a in db.OrderDetails
                            join b in db.Orders
                            on a.OrderId equals b.OrderId
                            join c in db.Products
                            on a.ProductId equals c.ProductId
                            join d in db.Users on b.UserId equals d.UserId
                            join e in db.Cards on d.UserId equals e.UserId
                            where b.StatusId == 5 && e.UserId == session.UserId
                            select new
                            {
                                ProductId = a.ProductId,
                                Price = a.Price,
                                Quantity = a.Quantity,
                                Discount = c.Discount,
                                NumberCard = e.NumberCard,
                                Username = d.Username
                            }).ToList();
                    if (models.Count()==0)
                    {
                        ViewBag.Card = 0;
                    }
                    else
                    {
                        double? total = 0;
                        foreach (var item in models)
                        {
                            total += ((item.Price.GetValueOrDefault(0) - (item.Price.GetValueOrDefault(0) * item.Discount.GetValueOrDefault(0) * 0.01)) * item.Quantity);
                        }
                      
                        model.NumberCard = Convert.ToInt32(total / 1000)- model.UserNumber;
                        db.SaveChanges();
                        ViewBag.Card = model.NumberCard;
                    }
               
            }
           else
            {
                return PartialView();
            }
            return PartialView();

        }




    }
}
