using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel;
using DiplomaDataModel.DataContext;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace OptionsWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChoicesController : Controller
    {
        private DiplomaOptionsContext db = new DiplomaOptionsContext();
        [OverrideAuthorization()]
        [Authorize(Roles = "Admin")]
        // GET: Choices
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FK_YearTermId).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption);
            return View(choices.ToList());
        }

        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.FourthOption).Include(c => c.FK_YearTermId);
            Choice choice = choices.Where(c => c.ChoiceId == id).First();
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }
        [OverrideAuthorization()]
        [Authorize(Roles = "Student,Admin")]
        // GET: Choices/Create
        public ActionResult Create()
        {
            var current = db.YearTerms.Where(c => c.isDefault == true).First();
            

            string term = "";
            if(current.Term == 10) {
                term += "Winter";
            }
            else if (current.Term == 20)
            {
                term += "Spring/Summer";
            }
            else{
                term += "Fall";
            }
            int yearTermId = current.YearTermId;

            ViewBag.FirstChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title");
            ViewBag.YearTermId = yearTermId;
            ViewBag.current = current.Term;
            ViewBag.yearTerm = term;
            ViewBag.FourthChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title");
            
            
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.studentId = user.UserName;
            
            return View();
        }
        [OverrideAuthorization()]
        [Authorize(Roles = "Student,Admin")]
        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {

            //set current date
            choice.SelectionDate = DateTime.Now;

            //getting current default term
            var current = db.YearTerms.Where(c => c.isDefault == true).First();

            //check option uniqueness
            Boolean canChoose = choosable(choice);

            //check if student already has an entry for the current term
            var check = checkDuplicate(choice.YearTermId);
            if (check.Count() > 0)
            {
                ModelState.AddModelError("", "You've already made a selection this term.");
            }
            //check if the options are all different
            else if (!canChoose)
            {
                ModelState.AddModelError("", "The options you chose must all be different");
            }

            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.FirstChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            ViewBag.FourthChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.ThirdChoiceOptionId);
            return View(choice);
        }

        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            ViewBag.FourthChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.ThirdChoiceOptionId);
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            //set current date
            choice.SelectionDate = DateTime.Now;

            //check option uniqueness
            Boolean canChoose = choosable(choice);

            //check if student already has an entry for the current term
            var check = checkDuplicate(choice.YearTermId);
            if (check.Count() > 1)
            {
                ModelState.AddModelError("", "You've already made a selection this term.");
            }
            //check if the options are all different
            else if (!canChoose)
            {
                ModelState.AddModelError("", "The options you chose must all be different");
            }

            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FirstChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            ViewBag.FourthChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title", choice.ThirdChoiceOptionId);
            return View(choice);
        }

        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.FourthOption).Include(c => c.FK_YearTermId);
            Choice choice = choices.Where(c => c.ChoiceId == id).First();
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //query db for active options for the drop down list
        private IQueryable<Option> getActiveOptions()
        {
            return db.Options.Where(ao => ao.isActive == true);
        }

        //check choice table for existing entries for the student in the current term
        private IQueryable<Choice> checkDuplicate(int yeartermid)
        {
            return db.Choices.Where(c => c.StudentId == User.Identity.Name && c.YearTermId == yeartermid);
        }

        private bool choosable(Choice choice)
        {
            HashSet<int> choiceSet = new HashSet<int>();

            choiceSet.Add((int)choice.FirstChoiceOptionId);
            choiceSet.Add((int)choice.SecondChoiceOptionId);
            choiceSet.Add((int)choice.ThirdChoiceOptionId);
            choiceSet.Add((int)choice.FourthChoiceOptionId);

            if( choiceSet.Count == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
