using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DawProject.Models;
using DawProject.Data;

namespace DawProject.Controllers
{
    public class SignInController : Controller
    {
        private LibraryContext context = new LibraryContext();
        // GET: SignIn
        public ActionResult Index()
        {
            bool state = Session["Email"] == null || Session["ID"] == null || Session["Password"] == null || Session["FullName"] == null || Session["BDate"] == null;
            if (!state)
                return RedirectToAction("Dashboard", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Index(SignIn reg)
        {
            bool state = Session["Email"] == null || Session["ID"] == null || Session["Password"] == null || Session["FullName"] == null || Session["BDate"] == null;
            if(!state)
                return RedirectToAction("DashBoard", "Home");
            var login_Params = context.Register.Select(x => new
            {
                Id = x.Id,
                Mail = x.Email,
                Pass = x.Password,
                Full_Name = x.FirstName + " " + x.LastName,
                BDate = x.BornDate
            }).ToList();
             
            //var SelectList = new SelectList(login_Params, "Mail", "Pass");
            if (login_Params != null)
            {
                for (int i = 0; i < login_Params.Count; i++)
                {
                    if (login_Params[i].Mail.Contains(reg.Email) && reg.Email.Contains(login_Params[i].Mail) && login_Params[i].Pass.Contains(reg.Password) && reg.Password.Contains(login_Params[i].Pass))
                    { 
                        Session["Email"] = login_Params[i].Mail.ToString();
                        Session["ID"] = login_Params[i].Id.ToString();
                        Session["Password"] = login_Params[i].Pass.ToString();
                        Session["FullName"] = login_Params[i].Full_Name.ToString();
                        Session["BDate"] = login_Params[i].BDate.ToString();
                        Session["Role"] = "User"; 
                        var roleWhere = context.Role.Select(x => new
                        {
                            emp = x.employee_id,
                            role = x.role_name
                        }).ToList();
                        if (roleWhere != null)
                        {
                            //ViewBag["EmployeeRoles"] = roleWhere.ToList();
                            for (int j = 0; j < roleWhere.Count; ++j)
                            {
                                if (roleWhere[j].emp == login_Params[i].Id)
                                {
                                    Session["Role"] = roleWhere[j].role;
                                    break;
                                }
                            }
                        } 
                        Logs newLog = new Logs();
                        newLog.text_log = login_Params[i].Full_Name.ToString() + " has logged in" + " at " + DateTime.Now.ToString();
                        context.Data_Logs.Add(newLog);
                        context.SaveChanges();
                        
                        return RedirectToAction("Dashboard","Home");
                    }
                }
            }
            return RedirectToAction("Index", "SignIn");
        }
    }
     

}