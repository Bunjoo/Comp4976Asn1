using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel;
using OptionsWebSite.DataContext;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace OptionsWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChoicesController : Controller
    {
        private DiplomaOptionsContext db = new DiplomaOptionsContext();
        [OverrideAuthorization()]
        [Authorize(Roles = "Student,Admin")]
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
            Choice choice = db.Choices.Find(id);
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
            if(current.YearTermId == 10) {
                term += "Winter";
            }
            else if (current.YearTermId == 20)
            {
                term += "Sping/Summer";
            }
            else {
                term += "Fall";
            }
            int yearTermId = current.YearTermId;

            ViewBag.FirstChoiceOptionId = new SelectList(getActiveOptions(), "OptionId", "Title");
            ViewBag.YearTermId = yearTermId;
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

            choice.SelectionDate = DateTime.Now;
            Boolean canChoose = choosable(choice);

            if (ModelState.IsValid && canChoose)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "The options you chose must all be different");
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
            Boolean canChoose = choosable(choice);
            if (ModelState.IsValid && canChoose)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "You cannot choose two of the same option.");
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
            Choice choice = db.Choices.Find(id);
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

        private IQueryable<Option> getActiveOptions()
        {
            return db.Options.Where(ao => ao.isActive == true);
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
