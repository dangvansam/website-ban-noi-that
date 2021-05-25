using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
namespace WebsiteNoiThat.Controllers
{
    public class CategoryProductController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
        
            var model = new CategoryDao().ListCategory();
            return PartialView(model);
        }
       /* public ActionResult Category()
        {

        }*/
    }
}