using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
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
            try
            {
                //sifrei python ile sifleme islemi 
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "C:\\Users\\Mahmud\\Anaconda3\\python.exe";

                string cmd = "C:\\Users\\Mahmud\\Desktop\\ChatCrypto\\ChatCrypto.WebUI\\ChatCrypto.WebUI\\python\\sifrelemeYontemleri.py";

                // ilk arguman sifreleme algoritmasi belirlemek icin
                // 0 -> sha algoritmalari

                // ikinci arguman sifreleme funksionu belirlemek icin
                // 0 -> sha224
                // 1 -> sha256
                // 2 -> sha384
                // 3 -> sha512

                // ucuncu arguman sifreleneecl metin

                string args = " 0 0 " + Password;

                start.Arguments = string.Format("{0} {1}", cmd, args);

                start.UseShellExecute = false;
                start.CreateNoWindow = true;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

                using (Process process = Process.Start(start))
                {
                    string sifre;
                    StreamReader reader = process.StandardOutput;
                    do
                    {
                        sifre = reader.ReadLine();
                        if (!string.IsNullOrEmpty(sifre))
                        {
                            Password = sifre;
                        }
                        Thread.Sleep(3);
                    } while (!process.HasExited);

                }
            }
            catch (Exception)
            {

                throw;
            }
            //update the state of user when he logint to true 
            var data = db.Accounts.Where(x => x.Username == Username && x.Password == Password).FirstOrDefault();
            if (data != null)
            {
                
                Session["Username"] = data.Username;
                Session["Email"] = data.Email;
                Session["Gender"] = data.Gender == true? "Male" : "Female";

                var role_acc_tbl = db.Role_Account.Where(x => x.Account_id == data.ID).FirstOrDefault();

                var role_account = db.Role_Account.Find(role_acc_tbl.ID);

                if (role_acc_tbl == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
          
                if (role_account == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    role_account.Is_active = true;
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Session["Username"] = null;
                ViewBag.ErrorMessage = "Please make sure from the username or password.";
                return View();
            }
        }

        public ActionResult Logout()
        {

            var username = Session["Username"].ToString();
            var data = db.Accounts.Where(x => x.Username == username).FirstOrDefault();

            var role_acc_tbl = db.Role_Account.Where(x => x.Account_id == data.ID).FirstOrDefault();

            var role_account = db.Role_Account.Find(role_acc_tbl.ID);

            if (role_acc_tbl == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (role_account == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                role_account.Is_active = false;
                db.SaveChanges();
            }

            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }
    }
}