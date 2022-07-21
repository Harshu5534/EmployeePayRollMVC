using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmployeeRL:IEmployeeRL
    {
        private readonly IConfiguration configuration;
        public EmployeeRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // To add New Employee Record
        public string AddEmployee(EmployeeModel employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:EmployeePayrollMVC"]))
                {
                    SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emp_name", employee.Emp_name);
                    cmd.Parameters.AddWithValue("@Profile_img", employee.Profile_img);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();
                    if(result>=1)
                    {
                        return "Data Added";
                    }
                    else
                    {
                        return "Data Not Added";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteEmployee(int? id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:EmployeePayrollMVC"]))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emp_Id", id);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result >= 1)
                    {
                        return "Data Added";
                    }
                    else
                    {
                        return "Data Not Added";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // To View All Employee Details
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            try
            {
                List<EmployeeModel> listemployee = new List<EmployeeModel>();
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:EmployeePayrollMVC"]))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        EmployeeModel employee = new EmployeeModel();

                        employee.Emp_id = Convert.ToInt32(rdr["Emp_id"]);
                        employee.Emp_name = Convert.ToString(rdr["Emp_name"]);
                        employee.Profile_img = Convert.ToString(rdr["Profile_img"]);
                        employee.Gender = Convert.ToString(rdr["Gender"]);
                        employee.Department = Convert.ToString(rdr["Department"]);
                        employee.Salary = Convert.ToInt32(rdr["Salary"]);
                        employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                        employee.Notes = Convert.ToString(rdr["Notes"]);

                        listemployee.Add(employee);
                    }
                    con.Close();
                }
                return listemployee;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EmployeeModel GetEmployeeData(int? id)
        {
            try
            {
                EmployeeModel employee = new EmployeeModel();
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:EmployeePayrollMVC"]))
                {
                    string sqlQuery = "SELECT * FROM EmployeeData WHERE Emp_id= " + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        employee.Emp_id = Convert.ToInt32(rdr["Emp_id"]);
                        employee.Emp_name = Convert.ToString(rdr["Emp_name"]);
                        employee.Profile_img = Convert.ToString(rdr["Profile_img"]);
                        employee.Gender = Convert.ToString(rdr["Gender"]);
                        employee.Department = Convert.ToString(rdr["Department"]);
                        employee.Salary = Convert.ToInt32(rdr["Salary"]);
                        employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                        employee.Notes = Convert.ToString(rdr["Notes"]);
                    }
                }
                return employee;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:EmployeePayrollMVC"]))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emp_id", employee.Emp_id);
                    cmd.Parameters.AddWithValue("@Emp_name", employee.Emp_name);
                    cmd.Parameters.AddWithValue("@Profile_img", employee.Profile_img);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result >= 1)
                    {
                        return "Data Added";
                    }
                    else
                    {
                        return "Data Not Added";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
