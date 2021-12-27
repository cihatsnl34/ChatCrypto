using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatCrypto.WebUI.Models;
namespace ChatCrypto.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        chat_dbEntities db = new chat_dbEntities();
        // GET: Register
       
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return View(account);
        }
    }
}