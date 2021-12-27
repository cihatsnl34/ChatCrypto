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
        public ActionResult Register(Role_Account role_Account)
        {
            if (ModelState.IsValid)
            {

                role_Account.Role_id = 2;
                db.Role_Account.Add(role_Account);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return View(role_Account);
        }
    }
}