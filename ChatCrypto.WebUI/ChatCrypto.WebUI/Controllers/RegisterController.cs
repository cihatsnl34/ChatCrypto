using System;
using System.Collections.Generic;
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
    public class RegisterController : Controller
    {
        chat_dbEntities db = new chat_dbEntities();
        // GET: Register
       
        public ActionResult Register()
        {
            if (TempData["error"] == null)
            {
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = TempData["error"];
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {

            if (ModelState.IsValid)
            {
                TempData["error"] = null;

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

                    string args = "0 0" + account.Password;

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
                                account.Password = sifre;
                            }
                            Thread.Sleep(3);
                        } while (!process.HasExited);

                    }

                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Login");

                }
                catch (Exception)
                {
                    TempData["error"] = "The username is allrady used. Please try somthing else.";
                    return RedirectToAction("Register", "Register");
                }
                
                
            }
            return View(account);
   
        }
    }
}