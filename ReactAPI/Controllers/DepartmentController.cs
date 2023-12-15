using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using ReactAPI.Models;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase

    {
        //public static List<Department> DepartmentList = new List<Department>();
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
             List<Department> DepartmentList = new List<Department>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select * from dbo.Department";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var Department = new Department();
                            Department.ID = int.Parse(dbDataReader["DepartmentID"].ToString());
                            Department.Name = dbDataReader["DepartmentName"].ToString();
                            DepartmentList.Add(Department);
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

            return new JsonResult(DepartmentList);
        }

        [HttpPost]

        public JsonResult Post(Department xdepartment)
        {
            //string query = @"
            // select DepartmentID, DepartmentName from dbo.Department
            //";

            //DataTable table = new DataTable();
            List<Department> DepartmentList = new List<Department>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into dbo.Department values (@Department)");
                        cmd.Parameters.AddWithValue("@Department", xdepartment.Name);
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

        public JsonResult Put(Department xdepartment)
        {
            //string query = @"
            // select DepartmentID, DepartmentName from dbo.Department
            //";

            //DataTable table = new DataTable();
            List<Department> DepartmentList = new List<Department>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update dbo.Department set DepartmentName = @DepartmentName where DepartmentID=@DepartmentID");
                        cmd.Parameters.AddWithValue("@DepartmentID", xdepartment.ID);
                        cmd.Parameters.AddWithValue("@DepartmentName", xdepartment.Name);
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
            //string query = @"
            // select DepartmentID, DepartmentName from dbo.Department
            //";

            //DataTable table = new DataTable();
            List<Department> DepartmentList = new List<Department>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from dbo.Department  where DepartmentID=@DepartmentID");
                        cmd.Parameters.AddWithValue("@DepartmentID", id);
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
    }
}

