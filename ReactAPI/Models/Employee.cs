using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public string Department { get; set; }

        public string DateOfJoining { get; set; }

        public string PhoteFileName { get; set; }
    }
}