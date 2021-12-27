using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatCrypto.WebUI.Models;
namespace ChatCrypto.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private chat_dbEntities db = new chat_dbEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            var data = db.Accounts.Where(x => x.Username == Username && x.Password == Password).ToList();
            if (data.Count == 1)
            {
                Session["AdminGiris"] = data.FirstOrDefault();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["User"] = null;
                ViewBag.ErrorMessage = "Lütfen şifrenizi veya kullanıcı adınızı doğru girdiğinizden emin olunuz.";
                return View(data);
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }
    }
}