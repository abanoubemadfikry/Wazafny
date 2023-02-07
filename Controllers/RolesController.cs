using Job_Offers_Website.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Job_Offers_Website.Controllers
{
    [Authorize(Roles ="Admins")]
    public class RolesController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            
            return View(_context.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            
                if (ModelState.IsValid)
                {

                    _context.Roles.Add(role);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
            
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
                return View();
            
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = _context.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
                IdentityRole role = _context.Roles.Find(id);
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return RedirectToAction("Index");  
        }
    }
}
