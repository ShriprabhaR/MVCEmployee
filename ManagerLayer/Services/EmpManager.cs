using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoryLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class EmpManager : IEmpManager
    {
        private readonly IEmpRepository emp;

        public EmpManager(IEmpRepository emp)
        {
            this.emp = emp;
        }
        public bool AddEmployee(AddEmployeeModel model)
        {
            return emp.AddEmployee(model);
        }
        public EmpModel GetEmployeeDetailsById(int EmployeeId)
        {
            return emp.GetEmployeeDetailsById(EmployeeId);
        }
        public List<EmpModel> GetAllEmployees()
        {
            return emp.GetAllEmployees();
        }
        public bool UpdateEmployee(EmpModel employee)
        {
            return emp.UpdateEmployee(employee);
        }
        public bool DeleteEmployee(int? id)
        {
            return emp.DeleteEmployee(id);
        }
        public LoginModel Login(int EmployeeId, string Name)
        {
            return emp.Login( EmployeeId, Name);
        }
        public EmpModel GetEmployeeById(int EmployeeId)
        {
            return emp.GetEmployeeById(EmployeeId);
        }
        public bool AddOrUpdateEmployee(EmpModel employee)
        {
           return emp.AddOrUpdateEmployee(employee);
        }
    }
}
