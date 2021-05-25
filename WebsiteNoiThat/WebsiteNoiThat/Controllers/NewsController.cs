using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;

namespace WebsiteNoiThat.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        DBNoiThat db = new DBNoiThat();
        public ActionResult NewsHot()
        {
            var model = new NewsDao().NewsHot();
            return PartialView(model);
        }
        public ActionResult Show(int NewsId)
        {
            var model = db.News.SingleOrDefault(n => n.NewsId == NewsId);
            return View(model);
        }

    }
}