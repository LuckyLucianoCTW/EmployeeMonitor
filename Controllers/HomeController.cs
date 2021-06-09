using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Reflection;

using DawProject.Models;
using DawProject.Data;
namespace DawProject.Controllers
{
    public class HomeController : Controller
    {
        SendMail mail_to_send = new SendMail();
        private LibraryContext context = new LibraryContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            Session["Email"] = null;
            Session["ID"] = null;
            Session["Password"] = null;
            Session["FullName"] = null;
            Session["BDate"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult profile()
        {
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Settings(ChangePassword newPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var my_user = context.Register.Find(Int32.Parse(Session["ID"].ToString()));
                    if (my_user == null)
                    {
                        return HttpNotFound();
                    }
                    if (!my_user.Password.Contains(newPassword.Password) || !newPassword.Password.Contains(my_user.Password))
                    {
                        Session["Email"] = null;
                        Session["ID"] = null;
                        Session["Password"] = null;
                        Session["FullName"] = null;
                        Session["BDate"] = null;
                        return RedirectToAction("Index", "Home");
                    }
                    my_user.Password = newPassword.ConfirmPassword;
                    my_user.ConfirmPassword = newPassword.ConfirmPassword; 
                    context.SaveChanges();


                    Logs newLog = new Logs();
                    newLog.text_log = Session["FullName"].ToString() + " changed his password at " + DateTime.Now.ToString();

                    context.Data_Logs.Add(newLog);
                    context.SaveChanges();
                    return RedirectToAction("Dashboard", "Home");

                }
            }
            catch (Exception e)
            {
                return Json(new { error_message = e.Message }, JsonRequestBehavior.AllowGet);
            }
            return View(newPassword);
        }
        [HttpGet]
        public ActionResult Projects()
        {
            if (Session["Email"] == null || Session["ID"] == null || Session["Password"] == null || Session["FullName"] == null || Session["BDate"] == null)
                return RedirectToAction("Dashboard", "Home");
            var login_Params = context.Register.Select(x => new
            {
                EmployeeID = x.Id,
                Mail = x.Email,
                Pass = x.Password,
                Full_Name = x.FirstName + " " + x.LastName,
                BDate = x.BornDate
            }).ToList();

            ViewBag.EmployeeList = new SelectList(login_Params, "EmployeeID", "Full_Name");
            var All_ProjectList = context.Project.Select(x => new
            {
                ProjectID = x.id,
                ProjectName = x.ProjectName,
                ProjDescrip = x.ProjectDescription,
                Start_Date = x.StartDate,
                End_Date = x.EndDate
            }).ToList();
            int myEmployeeID = Int32.Parse(Session["ID"].ToString());
            var AssignedProjectTo = context.ProjectAssigned.Where(x=> x.EmployeeId == myEmployeeID).Select(x => new
            {
                ProjectID = x.ProjectId
            }).ToList();
            List<Projects> projectList = new List<Projects>();
            for (var i = 0; i < AssignedProjectTo.Count; i++)
            {
                var ProjectDetails = context.Project.Find(AssignedProjectTo[i].ProjectID);
                projectList.Add(ProjectDetails);
            }
            ViewData["myProjectList"] = projectList.ToList();
            ViewBag.ProjectNames = new SelectList(All_ProjectList, "ProjectID", "ProjectName");
            ViewBag.ProjectDates = new SelectList(All_ProjectList, "Start Date", "Start_Date");
            ViewBag.ProjectDescriptions = new SelectList(All_ProjectList, "Project Description","ProjDescrip");
            return View();
        }

         
        [HttpPost]  
        public ActionResult Projects(Projects theProject)
        {
            if (ModelState.IsValid)
            {

                Logs newLog = new Logs();
                newLog.text_log = Session["FullName"].ToString() + " created project : " + theProject.ProjectName + " at " + DateTime.Now.ToString();
                context.Data_Logs.Add(newLog); 
                context.Project.Add(theProject);
                context.SaveChanges();
            }

            return RedirectToAction("Projects", "Home");
        }

        [ActionName("AssignProject")]
        [HttpPost]
        public ActionResult Projects(ProjectsAssigned theProject)
        {
            if (ModelState.IsValid)
            { 
                var employee = context.Register.Find(theProject.EmployeeId);
                if(employee == null)
                    return RedirectToAction("Projects", "Home");
                var project = context.Project.Find(theProject.ProjectId);
                if (project == null)
                    return RedirectToAction("Projects", "Home");
                Logs newLog = new Logs();
                var text_assigned = "You have been assigned to do the project " + project.ProjectName + " due to " + project.EndDate.ToString();
                mail_to_send.SendEmail(employee.Email, "Assign Project", text_assigned);
         
                newLog.text_log = Session["FullName"].ToString() + " assigned project : " + project.ProjectName + " to " + employee.FirstName + " " + employee.LastName + " at " + DateTime.Now.ToString();

                context.ProjectAssigned.Add(theProject);
                context.Data_Logs.Add(newLog);
                context.SaveChanges();
                return RedirectToAction("Projects", "Home");
            } 
            
            return RedirectToAction("Projects", "Home");
        }
        [ActionName("RemoveAssignProject")]
        [HttpPost]
        public ActionResult RemoveAssignProject(ProjectsAssigned theProject)
        { 
            if(ModelState.IsValid)
            {
                var log = context.ProjectAssigned.Where(x => (x.EmployeeId == theProject.EmployeeId && x.ProjectId == theProject.ProjectId)).ToList();
                if(log != null)
                {
                    if (log.Count > 0)
                    {
                        context.ProjectAssigned.Remove(log[0]);
                        context.SaveChanges();
                    }
                } 
            }
            return RedirectToAction("Projects", "Home");
        }
    }
}
