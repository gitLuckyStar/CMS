using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.Security;

namespace CMS.Controllers
{
    public class AccountController : Controller
    {
        private ILoginRepository loginRepository;
        CmsContext db = new CmsContext();
        public AccountController()
        {
            loginRepository = new LoginRepository(db);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if(ModelState.IsValid)
            {
                if(loginRepository.IsUserExistUser(login.Username,login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Username,login.RememberMe);
                    return  Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("Username", "کاربری یافت نشد");
                }
            }


            return View(login);
        }
        public ActionResult signout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}