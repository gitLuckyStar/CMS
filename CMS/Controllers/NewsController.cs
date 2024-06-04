using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace CMS.Controllers
{
    public class NewsController : Controller
    {
        CmsContext db = new CmsContext();
        private IPageGroupRepostiory pageGroupRepostiory;
        private IPageRepository pageRepository;
        private IPageCommentRepository pageCommentRepository;

        public NewsController()
        {
            pageGroupRepostiory = new PageGroupRepository(db);
            pageRepository = new PageRepository(db);
            pageCommentRepository = new PageCommentRepository(db);
        }
        // GET: News
        public ActionResult ShowGroups()
        {
            return PartialView(pageGroupRepostiory.GetGroupsForView());
        }
        public ActionResult ShowGroupsInDropDown()
        {
            return PartialView(pageGroupRepostiory.GetAllGroups());
        }
        public ActionResult TopNews()
        {
            return PartialView(pageRepository.TopNews());
        }
        public ActionResult LastNews()
        {
            return PartialView(pageRepository.LastNews());
        }

        [Route("Archive")]
        public ActionResult Archive()
        {
            return View(pageRepository.LastNews());
        }
        [Route("Group/{id}/{title}")]
        public ActionResult ShowNewsByGroupId(int id, string title)
        {
            ViewBag.Name = title;
            return View(pageRepository.ShowPageByGroupId(id));
        }
        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            var news = pageRepository.GetPageById(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            news.visit += 1;
            pageRepository.UpdatePage(news);
            pageRepository.save();

            return View(news);

        }
        public ActionResult AddComment(int id, string name, string email, string comment)
        {
            PageComment addcomment = new PageComment()
            {
                CreateDate = DateTime.Now,
                PageID = id,
                Name = name,
                Email = email,
            };

            pageCommentRepository.AddComment(addcomment);

            return PartialView("ShowComments",id);
        }
        public ActionResult ShowComments(int id)
        {
            return PartialView(pageCommentRepository.GetCommentsByNewsId(id));
        }
    }
}