using AdventureWorks.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using AdventureWorks.EFModell;

namespace AdventureWorks.DAL
{
    public class EmployeeDepartmentHistoryContent
    {
        /// <summary>
        /// Determination of the employees departmenthistory data records using the passed parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of all found records</returns>
        public List<EmployeeDepartmentHistorys> getFullDepartmentHistoryByEmployeeId(int id)
        {
            IEnumerable<EmployeeDepartmentHistorys> content = null;

            using (AdventureWorksCyclesEntities dbContext = new AdventureWorksCyclesEntities())
            {
                content = (from s in dbContext.EmployeeDepartmentHistory
                           select new EmployeeDepartmentHistorys
                           {
                               BusinessEntityID = s.BusinessEntityID,
                               DepartmentID = s.DepartmentID,
                               Name = s.Department.Name,
                               ShiftId = s.ShiftID,
                               EndDate = s.EndDate,
                               StartDate = s.StartDate,
                               ModificationDate = s.ModifiedDate,
                           })
                           .Where(s=> s.BusinessEntityID.Equals(id));

                return content.ToList();
            }

        }
    }
}