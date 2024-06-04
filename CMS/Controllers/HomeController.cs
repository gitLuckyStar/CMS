using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        CmsContext db = new CmsContext();
        private IPageRepository pageRepository;

        public HomeController()
            {
            pageRepository = new PageRepository(db);
            }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ShowInSlider()
        {
            return PartialView(pageRepository.ShowInSlider());
        }
    }
}