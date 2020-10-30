using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DepartamentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"get_departament";
            DataTable table = new DataTable();

            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
                using(var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Departament dep)
        {
            try
            {
                string query = @"add_departament";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = dep.DepartamentName
                    };
                    cmd.Parameters.Add(nameParam);
                    da.Fill(table);

                    return "Added Sucsesful";
                }

            }
            catch (Exception)
            {
                return "Error Added";
            }
        }

        public string Put(Departament dep)
        {
            try
            {
                string query = @"update_departament";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = dep.DepartamentName,
                    };

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = dep.DepartamentID
                    };

                    cmd.Parameters.Add(idParam);
                    cmd.Parameters.Add(nameParam);
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
                string query = @"delete_departament";
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
    }
}
