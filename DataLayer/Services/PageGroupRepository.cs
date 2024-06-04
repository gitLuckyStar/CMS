using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageGroupRepository : IPageGroupRepostiory
    {
        private CmsContext db;

        public PageGroupRepository(CmsContext context)
        {
            this.db = context;
        }

        public IEnumerable<PageGroup> GetAllGroups()
        {
            return db.pageGroups;
        }

        public PageGroup GetGroupById(int groupId)
        {
            return db.pageGroups.Find(groupId);
        }

        public bool InsertGroup(PageGroup pageGroup)
        {
            try
            {
                db.pageGroups.Add(pageGroup);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateGroup(PageGroup pageGroup)
        {
            try
            {
                db.Entry(pageGroup).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            try
            {
                var group = GetGroupById(groupId);
                DeleteGroup(group);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteGroup(PageGroup pageGroup)
        {
            try
            {
                db.Entry(pageGroup).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<ShowGroupsViewModel> GetGroupsForView()
        {
            return db.pageGroups.Select(G => new ShowGroupsViewModel()
            {
                GroupID = G.GroupID,
                GroupTitle = G.GroupTitle,
                PageCount = G.Pages.Count,
            });

        }
    }
}
