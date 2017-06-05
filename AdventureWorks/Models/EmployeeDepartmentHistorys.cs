using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Models
{
    public class EmployeeDepartmentHistorys
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int ShiftId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModificationDate { get; set; }
}
}