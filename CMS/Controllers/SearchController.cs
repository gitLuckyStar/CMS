using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace CMS.Controllers
{
    public class SearchController : Controller
    {
        IPageRepository pageRepository;
        CmsContext db = new CmsContext();

        public SearchController()
            {
            pageRepository = new PageRepository(db);
            }

        // GET: Search
        public ActionResult Index(string q)
        {
            ViewBag.Name = q;
            return View(pageRepository.SearchPages(q));
        }
    }
}