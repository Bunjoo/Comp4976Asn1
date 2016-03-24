using System;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using DiplomaDataModel;

namespace OptionsWebSite.Controllers
{

    /* TODO
        - prevent admin role from being edited/deleted
     */

    public class RolesController : Controller
    {
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        // GET: Roles
        public ActionResult Index()
        {
            var roles = dbcontext.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                dbcontext.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                dbcontext.SaveChanges();
                ViewBag.ResultMessage = "The role has been created.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Choices/Delete/5
        public ActionResult Delete(string RoleName)
        {
            if (RoleName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var thisRole = dbcontext.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (thisRole == null)
            {
                return HttpNotFound();
            }


            return View(thisRole);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string RoleName)
        {
            var thisRole = dbcontext.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            dbcontext.Roles.Remove(thisRole);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = dbcontext.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                dbcontext.Entry(role).State = System.Data.Entity.EntityState.Modified;
                dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}