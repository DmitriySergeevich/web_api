using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Departament { get; set; }
        public string DateofJoin { get; set; }
        public string PhotoFileName { get; set; }

    }
}