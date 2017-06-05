using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdventureWorks.DAL;

namespace AdventureWorks.Controllers
{
    public class EmployeesController : Controller
    {

        public ActionResult Index(string sortOrder, string searchString)
        {
            // create new employeeContent object (DAL) 
            EmployeeContent epc = new EmployeeContent();

            // get all Employees (List)
            var employeeList = epc.getAllEmployees();

            // If the search string is not empty, the result set is reduced to the parameter to be searched in the "First name" and "Last name" columns.
            if (!String.IsNullOrEmpty(searchString))
            {
                employeeList = employeeList.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString)).ToList();
            }
            
            return View(employeeList);
        }

    }
}
