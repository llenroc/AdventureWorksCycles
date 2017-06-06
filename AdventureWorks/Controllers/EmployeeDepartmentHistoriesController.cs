using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdventureWorks.DAL;
using AdventureWorks.Models;

namespace AdventureWorks.Controllers
{
    public class EmployeeDepartmentHistoriesController : Controller
    {

        // GET: EmployeeDepartmentHistories/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // the id parameter is not null, so convert it to int
            int currId = (int)id;
            EmployeeDepartmentHistoryContent edhc = new EmployeeDepartmentHistoryContent();

            // Display all departments(list) in which the employee has worked in the past, ordered by startdate and enddate.
            var departmentList = edhc.getFullDepartmentHistoryByEmployeeId(currId).OrderBy(s => s.StartDate).ThenBy(n => n.EndDate);

            // If the list is empty, a corresponding confirmation is given
            if (departmentList == null)
            {
                return HttpNotFound();
            }

            EmployeeContent ec = new EmployeeContent();

            // The name of the current employee is stored temporarily.
            //--------------------------------------------------------------
            Employees empContent = ec.getEmployeeNameById(currId);
            ViewBag.EmployeeFirstName = empContent.FirstName;
            ViewBag.EmployeeLastName = empContent.LastName;
            //--------------------------------------------------------------

            return View(departmentList);
        }
    }
}
