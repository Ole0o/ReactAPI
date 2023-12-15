using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using ReactAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]

        public JsonResult Get()
        {

            List<Employee> EmployeeList = new List<Employee>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select EmployeeID, EmployeeName, Department,  CONVERT(VARCHAR(30),DateOfJoining,121) DateOfJoining, PhoteFileName from dbo.Employee";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var Employee = new Employee();
                            Employee.EmployeeID = int.Parse(dbDataReader["EmployeeID"].ToString());
                            Employee.EmployeeName = dbDataReader["EmployeeName"].ToString();
                            Employee.Department = dbDataReader["Department"].ToString();
                            Employee.DateOfJoining = dbDataReader["DateOfJoining"].ToString();
                            Employee.PhoteFileName = dbDataReader["PhoteFileName"].ToString();
                            EmployeeList.Add(Employee);
                        }



                        dbDataReader.Close();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult(EmployeeList);

        }

        [HttpPost]

        public JsonResult Post(Employee xemployee)
        {

            List<Employee> EmployeeList = new List<Employee>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into dbo.Employee(EmployeeName, Department, DateOfJoining, PhoteFileName) values " +
                            "(@EmployeeName, @Department, @DateOfJoining, @PhoteFileName)");
                        cmd.Parameters.AddWithValue("@EmployeeName", xemployee.EmployeeName);
                        cmd.Parameters.AddWithValue("@Department", xemployee.Department);
                        cmd.Parameters.AddWithValue("@DateOfJoining", xemployee.DateOfJoining);
                        cmd.Parameters.AddWithValue("@PhoteFileName", xemployee.PhoteFileName);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Insert succesfully");

        }

        [HttpPut]

        public JsonResult Put(Employee xemployee)
        {

            List<Employee> EmployeeList = new List<Employee>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update dbo.Employee set EmployeeName =@EmployeeName, Department=@Department," +
                            " DateOfJoining=@DateOfJoining, PhoteFileName=@PhoteFileName where EmployeeID=@EmployeeID");
                        cmd.Parameters.AddWithValue("@EmployeeID", xemployee.EmployeeID);
                        cmd.Parameters.AddWithValue("@EmployeeName", xemployee.EmployeeName);
                        cmd.Parameters.AddWithValue("@Department", xemployee.Department);
                        cmd.Parameters.AddWithValue("@DateOfJoining", xemployee.DateOfJoining);
                        cmd.Parameters.AddWithValue("@PhoteFileName", xemployee.PhoteFileName);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Update succesfully");


        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            
            List<Employee> DepartmentList = new List<Employee>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from dbo.Employee  where EmployeeID=@EmployeeID");
                        cmd.Parameters.AddWithValue("@EmployeeID", id);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Delete succesfully");

        }

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photo/" + filename;

                using( var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                
            }

            return new JsonResult("anonynous.png");
        }
    }
}
