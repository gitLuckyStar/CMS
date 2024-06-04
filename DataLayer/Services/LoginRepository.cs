using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LoginRepository : ILoginRepository
    {
        private CmsContext db;
        public LoginRepository(CmsContext context)
        {
            db = context;
        }
        public bool IsUserExistUser(string username, string password)
        {
            return db.AdminLogins.Any(u => u.Username == username&&u.Password == password);
        }
    }
}
