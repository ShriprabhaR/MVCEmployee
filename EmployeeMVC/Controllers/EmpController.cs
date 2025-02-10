using System;
using System.Collections.Generic;
using System.Linq;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class EmpController : Controller
    {
        private readonly IEmpManager manager;

        public EmpController(IEmpManager manager)
        {
            this.manager = manager;
        }
       
        
        //To get all employees
        public IActionResult GetAllEmployees()
        {
            List<EmpModel> lstEmployee = new List<EmpModel>();
            lstEmployee = manager.GetAllEmployees().ToList();

            return View(lstEmployee);
        }

        //To add Employee
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee([Bind] AddEmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                manager.AddEmployee(employee);
                return RedirectToAction("GetAllEmployees");
            }
            return View(employee);
        }

        //To get particular employee

        [HttpGet]
        public IActionResult GetEmployeeDetailsById(int id)
        {
            int? EmployeeId = HttpContext.Session.GetInt32("EmployeeId");
            EmpModel employee = manager.GetEmployeeDetailsById(id);


            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        //To Update Employee details
        [HttpGet]
        public IActionResult UpdateEmployee(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            EmpModel employee = manager.GetEmployeeDetailsById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(int id, [Bind] EmpModel employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                manager.UpdateEmployee(employee);
                return RedirectToAction("GetAllEmployees");
            }
            return View(employee);
        }


        //To delete Employee
        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            EmpModel employee = manager.GetEmployeeDetailsById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        public IActionResult DeleteConfirm(int? id)
        {
            manager.DeleteEmployee(id);
            return RedirectToAction("GetAllEmployees");
        }

        //login
        [HttpGet]
        public IActionResult Login(int id, string name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = manager.Login(id, name);
                    if (result != null)
                    {
                        HttpContext.Session.SetInt32("EmployeeId", result.EmployeeId);
                        HttpContext.Session.SetString("Name", result.Name);

                        return RedirectToAction("GetEmployeeDetailsById", "Employee", new { id = result.EmployeeId });

                    }

                    ViewBag.Error = "Invalid Employee ID or Name.";
                    return View();
                }
                return View();
            }

            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred: " + ex.Message;
                return View();
            }
        }




        [HttpGet]
        public IActionResult GetEmployeeById(int id)
        {
            EmpModel employee = manager.GetEmployeeDetailsById(id);

            if (employee == null)
            {
                ViewBag.ErrorMessage = "Employee not found.";
                return View("Error");
            }

            return View(employee);
        }


        [HttpGet]
        public IActionResult AddOrUpdateEmployeeForm(int id)
        {
            EmpModel employee = new EmpModel();

            employee = manager.GetEmployeeById(id);
            
            return View(employee);
        }

        [HttpPost]
        public IActionResult AddOrUpdateEmployee(EmpModel employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data.");
            }

             manager.AddOrUpdateEmployee(employee);

            return RedirectToAction("GetAllEmployees");
        }

        
    }
}
