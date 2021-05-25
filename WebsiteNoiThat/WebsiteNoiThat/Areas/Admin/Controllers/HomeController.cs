using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteNoiThat.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
            if ( session != null)
            {
                ViewBag.username = session.Username;
                return View();

            }
            else
            {
                return Redirect("~/Admin/Login");
            }
        }
        //public ActionResult Show()
        //{
        //    var session = (UserLogin)Session[WebsiteNoiThat.Common.Commoncontent.user_sesion_admin];
        //    if (session != null)
        //    {
        //        ViewBag.username = session.Username;
        //        return View();

        //    }
        //    else
        //    {
        //        return Redirect("~/Admin/Login");
        //    }
        //}

    }
}