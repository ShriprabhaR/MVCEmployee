using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CommonLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class EmpRepository : IEmpRepository
    {
        private readonly string connectionString;
        private readonly IConfiguration config;

        public EmpRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DBConnection");
        }

        //To add employee
        public bool AddEmployee(AddEmployeeModel model)

        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@Department", model.Department);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
        }

        //Get the details of a particular employee    
        public EmpModel GetEmployeeDetailsById(int EmployeeId)
        {
            EmpModel employee = new EmpModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spFetchEmployeeById", con); ;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeId = reader.GetInt32(0);
                        employee.Name = reader.GetString(1);
                        employee.Age = reader.GetInt32(2);
                        employee.Salary = reader.GetInt32(3);
                        employee.Email = reader.GetString(4);
                        employee.City = reader.GetString(5);
                        employee.Department = reader.GetString(6);
                        employee.Gender = reader.GetString(7);
                    }
                    return employee;
                }

                con.Close();
                return null;
            }

        }

        //To View all employees details      
        public List<EmpModel> GetAllEmployees()
        {
            List<EmpModel> lstemployee = new List<EmpModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmpModel employee = new EmpModel();

                    employee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    employee.Name = reader["Name"].ToString();
                    employee.Age = Convert.ToInt32(reader["Age"]);
                    employee.Salary = Convert.ToInt32(reader["Salary"]);
                    employee.Email = reader["Email"].ToString();
                    employee.City = reader["City"].ToString();
                    employee.Department = reader["Department"].ToString();
                    employee.Gender = reader["Gender"].ToString();

                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        //To Update Employee details 
        public bool UpdateEmployee(EmpModel employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);


                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                if (res == 0)
                {
                    return false;
                }
                return true;
            }
        }

        //To delete Employee details
        public bool DeleteEmployee(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                if (res == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public LoginModel Login(int EmployeeId, string Name)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                LoginModel emp = null;
                SqlCommand cmd = new SqlCommand("spEmployeeLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@Name", Name);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        emp = new LoginModel
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            Name = reader["Name"].ToString()
                        };
                    }

                }
                con.Close();
                return emp;

            }

        }

        public EmpModel GetEmployeeById(int EmployeeId)
        {
            EmpModel employee = new EmpModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetEmployeeById", con); ;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeId = reader.GetInt32(0);
                        employee.Name = reader.GetString(1);
                        employee.Age = reader.GetInt32(2);
                        employee.Salary = reader.GetInt32(3);
                        employee.Email = reader.GetString(4);
                        employee.City = reader.GetString(5);
                        employee.Department = reader.GetString(6);
                        employee.Gender = reader.GetString(7);
                    }
                    return employee;
                }

                con.Close();
                return null;
            }

        }

        public bool AddOrUpdateEmployee(EmpModel employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("AddOrUpdateEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);

                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }

    }
}
