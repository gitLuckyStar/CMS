using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        IPageRepository pageRepository;
        IPageGroupRepostiory pageGroupRepostiory;
        private CmsContext db = new CmsContext();

        public PagesController()
        {
            pageRepository = new PageRepository(db);
            pageGroupRepostiory = new PageGroupRepository(db);
        }
        
        // GET: Admin/Pages
        public ActionResult Index()
        {
            
            return View(pageRepository.GetAllPage());
        }

        // GET: Admin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(pageGroupRepostiory.GetAllGroups(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,Title,ShortDescription,visit,Text,ImageName,ShowInSlider,CreateDate,Tags")] Page page, HttpPostedFileBase imgUP)
        {
            if (ModelState.IsValid)
            {
                page.visit = 0;
                page.CreateDate = DateTime.Now;

                if(imgUP !=null)
                {
                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUP.FileName);
                    imgUP.SaveAs(Server.MapPath("/PageIMG/"+page.ImageName));
                }

                pageRepository.insertPage(page);
                pageRepository.save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.pageGroups, "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(pageGroupRepostiory.GetAllGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,Title,ShortDescription,visit,Text,ImageName,ShowInSlider,CreateDate,Tags")] Page page,HttpPostedFileBase imgUP)
        {
            if (ModelState.IsValid)
            {

                if (imgUP != null)
                {
                    if(page.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/PageIMG/" + page.ImageName));
                    }
                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUP.FileName);
                    imgUP.SaveAs(Server.MapPath("/PageIMG/" + page.ImageName));
                }

                pageRepository.UpdatePage(page);
                pageRepository.save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.pageGroups, "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = pageRepository.GetPageById(id);
            if (page.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/PageIMG/" + page.ImageName));
            }
            pageRepository.DeletePage(page);
            pageRepository.save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pageGroupRepostiory.Dispose();
                pageRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
