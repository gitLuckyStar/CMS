using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageCommentRepository : IPageCommentRepository
    {
        private CmsContext db;

        public PageCommentRepository(CmsContext context)
        {
            db = context;
        }
        public IEnumerable<PageComment> GetCommentsByNewsId(int pageId)
        {
            return db.pageComments.Where(c => c.PageID == pageId);
        }
        public bool AddComment(PageComment comment)
        {

            try
            {
                db.pageComments.Add(comment);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


    }
}
