using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AdventureWorks.DAL;
using System.Collections.Generic;
using AdventureWorks.Utils;
using AdventureWorks.Models;

namespace AdventureWorks.Controllers
{
    public class EmployeesController : Controller
    {

        public ActionResult Index(string sortOrder, string searchString)
        {
            // create new employeeContent object (DAL) 
            EmployeeContent epc = new EmployeeContent();

            // Determine all employees(List) using the DAL function 
            var employeeList = epc.getAllEmployees();
            List<string> firstLastNameList = new List<string>();

            // If the search string is not empty, the result set is reduced to the parameter to be searched in the "First name" and "Last name" columns.
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchingFor = "Tabelle zeigt aktuell alle Treffer mit '" + searchString + "'"; 

                // Return a list containing the person names with the values we are looking for
                employeeList = employeeList.Where(s => s.LastName.ToLower().Contains(searchString.ToLower()) || s.FirstName.ToLower().Contains(searchString.ToLower())).ToList();

                // first idea of getting the similarities
                foreach (Employees content in employeeList)
                {
                    content.similarity = FuzzySearch.CalculateSimilarity(searchString.ToLower(), (content.FirstName.ToLower() + " " + content.LastName.ToLower()));
                }
            }

            // return the employeelist
            return View(employeeList);
        }

    }
}
