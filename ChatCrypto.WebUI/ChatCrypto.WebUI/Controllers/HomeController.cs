using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ChatCrypto.WebUI.Models;
namespace ChatCrypto.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private chat_dbEntities db = new chat_dbEntities();
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var users = db.Accounts.ToList();
                var active_users = new List<Account>();
                foreach (var user in users)
                {
                    var check = db.Role_Account.Where(y => y.Account_id == user.ID).FirstOrDefault();
                    if (check.Is_active)
                    {
                        active_users.Add(user);
                    }
                }
                return View(active_users);
            }
            
        }

        public ActionResult PartialUsersList()
        {
            var users = db.Accounts.ToList();
            var active_users = new List<Account>();
            foreach (var user in users)
            {
                var check = db.Role_Account.Where(y => y.Account_id == user.ID).FirstOrDefault();
                if (check.Is_active)
                {
                    active_users.Add(user);
                }
            }
            return View(active_users);
        }
        [HttpPost]
        public ActionResult GetMessage(string content)
        {
            string username = Session["Username"].ToString();
            var user = db.Accounts.Where(u => u.Username == username).FirstOrDefault();

            //mesaglar python ile sifleme islemi 
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:\\Users\\Mahmud\\Anaconda3\\python.exe";

            string cmd = "C:\\Users\\Mahmud\\Desktop\\ChatCrypto\\ChatCrypto.WebUI\\ChatCrypto.WebUI\\python\\sifrelemeYontemleri.py";

            // ilk arguman sifreleme algoritma belirlemek icin
            // 1 -> Symmetric 

            // ikinci arguman sifrelenecek funksionu belirlemek icin
            // 0 -> decryption
            // 1 -> encryption

            // ucuncu arguman sifrelenecek veya desifrelenecek metin 
            string args = " 1 1 " + content; //sifreleme islemi

            start.Arguments = string.Format("{0} {1}", cmd, args);

            start.UseShellExecute = false;
            start.CreateNoWindow = true;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            Hash hash = new Hash();
            Messsage msg = new Messsage();
            using (Process process = Process.Start(start))
            {

                string sifre;
                StreamReader reader = process.StandardOutput;
                do
                {
                    Thread.Sleep(15);
                    sifre = reader.ReadLine();
                    if (!string.IsNullOrEmpty(sifre))
                    {
                        hash.Content = sifre;
                    }

                } while (!process.HasExited);

                var eklendi = db.Hashes.Add(hash);
                msg.Hash_id = eklendi.ID;
                msg.Sender_id = user.ID;
                msg.Msg_time = DateTime.Now;
                db.Messsages.Add(msg);
                db.SaveChanges();

            }


            return Json(new { success = true, message = "added "+ content }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialMessage()
        {
            //string username = Session["Username"].ToString();
            var msgs = db.Messsages.ToList();

            //mesaglar python ile sifleme islemi 
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:\\Users\\Mahmud\\Anaconda3\\python.exe";

            string cmd = "C:\\Users\\Mahmud\\Desktop\\ChatCrypto\\ChatCrypto.WebUI\\ChatCrypto.WebUI\\python\\sifrelemeYontemleri.py";

            // ilk arguman sifreleme algoritma belirlemek icin
            // 1 -> Symmetric 

            // ikinci arguman sifrelenecek funksionu belirlemek icin
            // 0 -> decryption
            // 1 -> encryption

            // ucuncu arguman sifrelenecek veya desifrelenecek metin 
            List<mesgs> mesgList = new List<mesgs>();
            foreach (var msg in msgs)
            {
                var user = db.Accounts.Where(u => u.ID == msg.Sender_id).FirstOrDefault();

                var msgContent = db.Hashes.Where(x => x.ID == msg.Hash_id).FirstOrDefault();

                string args = " 1 0 " + msgContent.Content; //desifre islemi

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
                        Thread.Sleep(15);
                        sifre = reader.ReadLine();
                        if (!string.IsNullOrEmpty(sifre))
                        {
                            mesgList.Add(new mesgs
                            {
                                id = (int)msg.Sender_id,
                                username = user.Username,
                                msg = sifre,
                                date = msg.Msg_time.ToString()

                            });
                        }
                        
                    } while (!process.HasExited);

                }
            }

            return View(mesgList);
        }

    }
}