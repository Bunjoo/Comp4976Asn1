using DiplomaDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OptionsWebSite.Controllers
{

    /*
    TODO:
    - populate dropdown list with student names
    - populate dropdown list with roles
    - use AddToRoleAsync with UserManager to add roles
        
     */

    public class UserRolesController : Controller
    {
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        // GET: UserRoles
        public ActionResult Index()
        {
            return View();
        }


        //user.Roles.Add(new IdentityUserRole() { RoleId = role.Id, UserId = user.Id });
    }
}