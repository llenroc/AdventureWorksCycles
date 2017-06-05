using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorks;
using AdventureWorks.DAL;
using AdventureWorks.Models;

namespace AdventureWorks.Controllers
{
    public class EmployeeDepartmentHistoriesController : Controller
    {
        private AdventureWorksCyclesEntities db = new AdventureWorksCyclesEntities();

        // GET: EmployeeDepartmentHistories
        public ActionResult Index()
        {
            var employeeDepartmentHistory = db.EmployeeDepartmentHistory.Include(e => e.Department).Include(e => e.Employee);
            return View(employeeDepartmentHistory.ToList());
        }

        // GET: EmployeeDepartmentHistories/Details/5
        public ActionResult Details(int? id)
        {
            int currId = 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            else
            {
                currId = (int)id;
            }

            EmployeeDepartmentHistoryContent edhc = new EmployeeDepartmentHistoryContent();

            var departmentList = edhc.getFullDepartmentHistoryByEmployeeId(currId).OrderBy(s => s.StartDate).ThenBy(n => n.EndDate);

            if (departmentList == null)
            {
                return HttpNotFound();
            }

            EmployeeContent ec = new EmployeeContent();
            // The name of the current employee is stored temporarily.
            Employees empContent = ec.getEmployeeNameById(currId);
            ViewBag.EmployeeFirstName = empContent.FirstName;
            ViewBag.EmployeeLastName = empContent.LastName;

            return View(departmentList);
        }

        // GET: EmployeeDepartmentHistories/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Department, "DepartmentID", "Name");
            ViewBag.BusinessEntityID = new SelectList(db.Employee, "BusinessEntityID", "NationalIDNumber");
            return View();
        }

        // POST: EmployeeDepartmentHistories/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,DepartmentID,ShiftID,StartDate,EndDate,ModifiedDate")] EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeDepartmentHistory.Add(employeeDepartmentHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Department, "DepartmentID", "Name", employeeDepartmentHistory.DepartmentID);
            ViewBag.BusinessEntityID = new SelectList(db.Employee, "BusinessEntityID", "NationalIDNumber", employeeDepartmentHistory.BusinessEntityID);
            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDepartmentHistory employeeDepartmentHistory = db.EmployeeDepartmentHistory.Find(id);
            if (employeeDepartmentHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Department, "DepartmentID", "Name", employeeDepartmentHistory.DepartmentID);
            ViewBag.BusinessEntityID = new SelectList(db.Employee, "BusinessEntityID", "NationalIDNumber", employeeDepartmentHistory.BusinessEntityID);
            return View(employeeDepartmentHistory);
        }

        // POST: EmployeeDepartmentHistories/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,DepartmentID,ShiftID,StartDate,EndDate,ModifiedDate")] EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeDepartmentHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Department, "DepartmentID", "Name", employeeDepartmentHistory.DepartmentID);
            ViewBag.BusinessEntityID = new SelectList(db.Employee, "BusinessEntityID", "NationalIDNumber", employeeDepartmentHistory.BusinessEntityID);
            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDepartmentHistory employeeDepartmentHistory = db.EmployeeDepartmentHistory.Find(id);
            if (employeeDepartmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(employeeDepartmentHistory);
        }

        // POST: EmployeeDepartmentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeDepartmentHistory employeeDepartmentHistory = db.EmployeeDepartmentHistory.Find(id);
            db.EmployeeDepartmentHistory.Remove(employeeDepartmentHistory);
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
    }
}
