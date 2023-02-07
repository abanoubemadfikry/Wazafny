using Job_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    
    public class HomeController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            
            return View(categories);
        }
        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var userid = User.Identity.GetUserId();
            var jobs = _context.ApplyForJobs.Where(a => a.UserId == userid);
            return View(jobs.ToList());
          

        }
        [Authorize]
        public ActionResult UserInfo()
        {
            //var userreg = new ApplicationUser();
            var userid = User.Identity.GetUserId();
            var currentuser = _context.Users.Where(a => a.Id == userid).SingleOrDefault();
            return View(currentuser);

        }
       
        [Authorize(Roles = "الباحثون")]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string Message, int jobid)
        {
            var job = new ApplyForJob();
            var UserId = User.Identity.GetUserId();

            var check = _context.ApplyForJobs.Where(a => a.JobId == jobid && a.UserId == UserId);

            if (check.Count() < 1)
            {
                //var JobId = (int)Session["JobId"];
                job.JobId = jobid;
                job.UserId = UserId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                _context.ApplyForJobs.Add(job);
                _context.SaveChanges();
                ViewBag.Result1 = "..تم ارسال طلبك و التقدم الي الوظيفه بنجاح";

            }
            else
            {
                ViewBag.Result2 = "sorry you are already applied for this job";
            }

            return View();
            /*return RedirectToAction("Index");*/
        }
        public ActionResult Detailsofjob(int id)
        {
            var job = _context.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [HttpGet]
        public ActionResult Editthejob(int id)
        {
            var job = _context.ApplyForJobs.Find(id);
            if (job==null)
            {
                return HttpNotFound(); 
            }
            return View(job);
        }
        [HttpPost]
        public ActionResult Editthejob(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                _context.Entry(job).State=EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Deletethejob(int id)
        {
            var job = _context.ApplyForJobs.Find(id);
   
            if (job == null)
            {  
                return HttpNotFound();
            }
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletethejob(ApplyForJob job)
        {
            ApplyForJob myjob = _context.ApplyForJobs.Find(job.Id);
            _context.ApplyForJobs.Remove(myjob);
            _context.SaveChanges();
            return RedirectToAction("GetJobsByUser");      
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact,RegisterViewModel model)
        {
            var userid = User.Identity.GetUserId();
            var currentuser=_context.Users.Where(a=>a.Id== userid).FirstOrDefault();    

            var mail = new MailMessage();
            var logininfo = new NetworkCredential(model.Email, model.Password);
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress(currentuser.Email));

            
            mail.Subject=contact.Subject;
            mail.Body = contact.Message;

            var smtpClient=new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = logininfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");   

        }
        public ActionResult Details(int jobid)
        {
            var item = _context.Jobs.FirstOrDefault(i => i.Id == jobid);
            if(item == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobid;
            return View(item);
        }
        [Authorize (Roles ="الناشرون")]
        public ActionResult GetJobsByPublisher()
        {
            var userid=User.Identity.GetUserId();
            var jobs = from app in _context.ApplyForJobs
                       join job in _context.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == userid
                       select app;
            //var groupped = jobs.GroupBy(a => a.Job.JobTitle);
            var groupped = from j in jobs
                           group j by j.Job.JobTitle
                         into gr
                           select new JobsViewModel
                           {
                               Jobtitle = gr.Key,
                               Items = gr
                           };
            return View(groupped.ToList());
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Search(string searchname)
        {
            var result = _context.Jobs.Where(a => a.JobTitle.Contains(searchname) ||
                                            a.JobContent.Contains(searchname) ||
                                            a.Category.CategoryDescription.Contains(searchname) ||
                                            a.Category.CategoryName.Contains(searchname)).ToList();
            ViewBag.numofjobs=result.Count;
            return View(result);
        }
        
        
    }
}
