using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"get_employees";
            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            try
            {
                string query = @"add_employee";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = emp.EmployeeName
                    };

                    SqlParameter departementParam = new SqlParameter
                    {
                        ParameterName = "@departement",
                        Value = emp.Departament
                    };

                    SqlParameter dateParam = new SqlParameter
                    {
                        ParameterName = "@DateOfJoin",
                        Value = emp.DateofJoin
                    };

                    SqlParameter photoParam = new SqlParameter
                    {
                        ParameterName = "@PhotoFileName",
                        Value = emp.PhotoFileName
                    };

                    cmd.Parameters.Add(nameParam);
                    cmd.Parameters.Add(departementParam);
                    cmd.Parameters.Add(dateParam);
                    cmd.Parameters.Add(photoParam);

                    da.Fill(table);

                    return "Added Sucsesful";
                }

            }
            catch (Exception)
            {
                return "Error Added";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"update_employee";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = emp.EmployeeId
                    };

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = emp.EmployeeName
                    };

                    SqlParameter departementParam = new SqlParameter
                    {
                        ParameterName = "@departement",
                        Value = emp.Departament
                    };

                    SqlParameter dateParam = new SqlParameter
                    {
                        ParameterName = "@DateOfJoin",
                        Value = emp.DateofJoin
                    };

                    SqlParameter photoParam = new SqlParameter
                    {
                        ParameterName = "@PhotoFileName",
                        Value = emp.PhotoFileName
                    };

                    cmd.Parameters.Add(idParam);
                    cmd.Parameters.Add(nameParam);
                    cmd.Parameters.Add(departementParam);
                    cmd.Parameters.Add(dateParam);
                    cmd.Parameters.Add(photoParam);
                    da.Fill(table);

                    return "Update Sucsesful";
                }
            }
            catch (Exception)
            {
                return "Error Update";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"delete_employee";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = id
                    };
                    cmd.Parameters.Add(idParam);
                    da.Fill(table);
                    return "Delete Sucsesful";
                }
            }
            catch (Exception)
            {
                return "Error Delete";
            }
        }

        [Route("api/Employee/GetAllDepartamentName")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartamentName()
        {
            string query = @"get_departament";
            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);

                postedFile.SaveAs(physicalPath);

                return fileName;
            }
            catch (Exception)
            {
                return "Error AddedPhoto";
            }
        }

    }
}
