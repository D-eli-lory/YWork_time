using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Working_time.Models;

namespace Working_time.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YProjectController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public YProjectController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ProjectID, ProjectName, Kod, ProjectActive from YProject";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table); 
        }
        [HttpPost]
        public JsonResult Post(YProject yp)
        {
            string query = @"insert into dbo.YProject
                            (ProjectName, Kod, ProjectActive)
                        values (@ProjectName, @Kod, @ProjectActive)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectName", yp.ProjectName);
                    myCommand.Parameters.AddWithValue("@Kod", yp.Kod);
                    myCommand.Parameters.AddWithValue("@ProjectActive", yp.ProjectActive);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Добавлено успешно");
        }

        [HttpPut]
        public JsonResult Put(YProject yp)
        {
            string query = @"update dbo.YProject
                            set ProjectName=@ProjectName, Kod=@Kod, ProjectActive=@ProjectActive
                             where ProjectID=@ProjectID";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectID", yp.ProjectID);
                    myCommand.Parameters.AddWithValue("@ProjectName", yp.ProjectName);
                    myCommand.Parameters.AddWithValue("@Kod", yp.Kod);
                    myCommand.Parameters.AddWithValue("@ProjectActive", yp.ProjectActive);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Успешно обновлено");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.YProject
                             where ProjectID=@ProjectID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Данные удалены");
        }
    }
}
