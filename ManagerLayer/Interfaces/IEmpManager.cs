using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace ManagerLayer.Interfaces
{
    public interface IEmpManager
    {
        public bool AddEmployee(AddEmployeeModel model);
        public EmpModel GetEmployeeDetailsById(int EmployeeId);
        public List<EmpModel> GetAllEmployees();
        public bool UpdateEmployee(EmpModel employee);
        public bool DeleteEmployee(int? id);
        public LoginModel Login(int EmployeeId, string Name);
        public EmpModel GetEmployeeById(int EmployeeId);
        public bool AddOrUpdateEmployee(EmpModel employee);
    }
}
