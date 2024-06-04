using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageRepository : IPageRepository
    {
        private CmsContext db;

        public PageRepository(CmsContext context)
        {
            this.db = context;
        }
        public IEnumerable<Page> GetAllPage()
        {
                return db.pages;
        }

        public Page GetPageById(int pageId)
        {
                return db.pages.Find(pageId);
        }

        public bool insertPage(Page page)
        {
            try
            {
                db.pages.Add(page);
                return true;
                
            }
            catch (Exception)
            {

                return false;
            };
        }

        public bool UpdatePage(Page page)
        {
            try
            {
                db.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            };
        }
        public bool DeletePage(Page page)
        {
            try
            {
                db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;

            };
        }
        public bool DeletePage(int pageId)
        {
            try
            {
                var page = GetPageById(pageId);
                DeletePage(page);
                return true;
            }
            catch (Exception)
            {

                return false;

            };
        }

        public void save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<Page> TopNews(int take =4)
        {
            return db.pages.OrderByDescending(p=> p.visit).Take(take);
        }

        public IEnumerable<Page> ShowInSlider()
        {
            return db.pages.Where(p => p.ShowInSlider == true);
        }

        public IEnumerable<Page> LastNews(int take = 4)
        {
            return db.pages.OrderByDescending(p => p.CreateDate).Take(take);
        }

        public IEnumerable<Page> ShowPageByGroupId(int groupId)
        {
            return db.pages.Where(p=>p.GroupID == groupId);
        }

        public IEnumerable<Page> SearchPages(string Search)
        {
            return db.pages.Where(p=>p.Title.Contains(Search)||p.ShortDescription.Contains(Search)||p.Tags.Contains(Search)||
            p.Text.Contains(Search)).Distinct();
        }
    }
}
