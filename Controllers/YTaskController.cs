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
    public class YTaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public YTaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select dbo.YTask.TaskID, dbo.YTask.TaskName, dbo.YTask.TaskActive, dbo.YTask.ProjectName
                            from dbo.YTask";
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
        public JsonResult Post(YTask yt)
        {
            string query = @"insert into dbo.YTask
                            (TaskName,TaskActive,ProjectName)
                            values (@TaskName,@TaskActive,@ProjectName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TaskName", yt.TaskName);
                    myCommand.Parameters.AddWithValue("@TaskActive", yt.TaskActive);
                    myCommand.Parameters.AddWithValue("@ProjectName", yt.ProjectName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Добавлено успешно");
        }
        [HttpPut]
        public JsonResult Put(YTask yt)
        {
            string query = @"update dbo.YTask
                            set TaskName=@TaskName, TaskActive=@TaskActive, ProjectName=@ProjectName
                            where TaskID=@TaskID";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TaskID", yt.TaskID);
                    myCommand.Parameters.AddWithValue("@TaskName", yt.TaskName);
                    myCommand.Parameters.AddWithValue("@TaskActive", yt.TaskActive);
                    myCommand.Parameters.AddWithValue("@ProjectName", yt.ProjectName);
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
            string query = @"delete from dbo.YTask
                             where TaskID=@TaskID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TaskID", id);
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
