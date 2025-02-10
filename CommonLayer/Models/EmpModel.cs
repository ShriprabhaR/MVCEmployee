using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace CommonLayer.Models
{
    public class EmpModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Age {  get; set; }
        public int Salary {  get; set; }
        public string Email {  get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
    }
}
