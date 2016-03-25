using System;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using DiplomaDataModel;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace OptionsWebSite.Controllers
{

    /* TODO
        - prevent admin role from being edited/deleted
     */

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ApplicationDbContext dbcontext = new ApplicationDbContext();
        object userManager;

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

        //set user manager
        public ApplicationUserManager UserManager
        {
            get
            { 
                return (ApplicationUserManager)userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        public ActionResult ManageUserRoles()
        {
            
            //populate users for dropdown
            var userList = dbcontext.Users.Select(c => new SelectListItem { Value = c.UserName.ToString(), Text = c.UserName.ToString() });

            //populate roles for dropdown
            var list = dbcontext.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
                        new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            ViewBag.Users = userList;
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {

            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(RoleName))
            {
                ApplicationUser user = dbcontext.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                UserManager.AddToRole(user.Id, RoleName);
            }

            //populate users for dropdown
            var userList = dbcontext.Users.Select(c => new SelectListItem { Value = c.UserName.ToString(), Text = c.UserName.ToString() });

            //populate roles for dropdown
            var list = dbcontext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            ViewBag.Roles = list;
            ViewBag.Users = userList;

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = dbcontext.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);
                ViewBag.userName = user.UserName;
            }

            //populate users for dropdown
            var userList = dbcontext.Users.Select(c => new SelectListItem { Value = c.UserName.ToString(), Text = c.UserName.ToString() });

            //populate roles for dropdown
            var list = dbcontext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            ViewBag.Roles = list;
            ViewBag.Users = userList;

            return View("ManageUserRoles");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string[] RoleName)
        {
            if (!string.IsNullOrWhiteSpace(UserName) && RoleName != null)
            {
                ApplicationUser user = dbcontext.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                foreach (var role in RoleName)
                {
                    if (UserManager.IsInRole(user.Id, role))
                    {
                        UserManager.RemoveFromRole(user.Id, role);
                    }
                }

            }

            //populate users for dropdown
            var userList = dbcontext.Users.Select(c => new SelectListItem { Value = c.UserName.ToString(), Text = c.UserName.ToString() });

            //populate roles for dropdown
            var list = dbcontext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            ViewBag.Roles = list;
            ViewBag.Users = userList;

            return View("ManageUserRoles");
        }

    }
}