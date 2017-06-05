using AdventureWorks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorks.DAL
{
    public class EmployeeContent
    {
        public List<Employees> getAllEmployees()
        {
            IQueryable<Employees> content = null;

            using (AdventureWorksCyclesEntities dbContext = new AdventureWorksCyclesEntities())
            {
                content = (from s in dbContext.Employee
                       select new Employees
                       {
                           BusinessEntityID = s.BusinessEntityID,
                           EmployeeID = s.NationalIDNumber,
                           FirstName = s.Person.FirstName,
                           MiddleName = s.Person.MiddleName,
                           LastName = s.Person.LastName,
                           LoginID = s.LoginID,
                           OrganizationLevel = s.OrganizationLevel,
                           JobTitle = s.JobTitle,
                           BirthDate = s.BirthDate,
                           Gender = s.Gender,
                           HireDate = s.HireDate,
                           ModificationDate = s.ModifiedDate,
                       });

                return content.ToList();
            }
        }

        public Employees getEmployeeNameById(int id)
        {
            using (AdventureWorksCyclesEntities dbContext = new AdventureWorksCyclesEntities())
            {
                return dbContext.Employee.Where(x => x.BusinessEntityID == id).Select(g => new Employees()
                {
                    FirstName = g.Person.FirstName,
                    MiddleName = g.Person.MiddleName,
                    LastName = g.Person.LastName
                }).FirstOrDefault<Employees>();
            }
        }
    }
}