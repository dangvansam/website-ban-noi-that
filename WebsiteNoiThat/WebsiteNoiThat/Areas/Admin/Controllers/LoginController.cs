using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Areas.Admin.Models;
using Models.DAO;
using WebsiteNoiThat.Common;
using System.Web.Security;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Username, Encryptor.MD5Hash(model.Passwword), true);
                if (result == 1)
                {
                    var user = dao.GetById(model.Username);
                    var userSession = new UserLogin();
                    userSession.Username = user.Username;
                    userSession.UserId = user.UserId;
                    Session["UserId"] = user.UserId.ToString();
                    userSession.GroupId = user.GroupId;
                    var listCredentials = dao.GetListCredentials(model.Username);
                    Session.Add(Commoncontent.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(Commoncontent.user_sesion_admin, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài Khoản không tồn tại!");

                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản đang bị khóa!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                }
            }

            return View("Index");
        }
        public ActionResult Logout()
        {
            Session[Commoncontent.user_sesion_admin] = null;
            Session[Commoncontent.SESSION_CREDENTIALS] = null;
           FormsAuthentication.SignOut();
            return RedirectToAction("Index","Login");
        }
         
    }
}