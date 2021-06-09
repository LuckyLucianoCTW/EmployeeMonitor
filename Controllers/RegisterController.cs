using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DawProject.Models;
using DawProject.Data;
namespace DawProject.Controllers
{
    public class RegisterController : Controller
    {
        private LibraryContext context = new LibraryContext();
        // GET: Register
        [HttpGet]
        public ActionResult Index()
        { 
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegisterE reg)
        {
            if(ModelState.IsValid)
            {
                context.Register.Add(reg); 
                Logs newLog = new Logs();
                newLog.text_log = reg.FirstName + " " + reg.LastName + " has registered";
                context.Data_Logs.Add(newLog);
                context.SaveChanges();
                return RedirectToAction("Index", "SignIn");
            }
            return View(reg);
        }
    }
}