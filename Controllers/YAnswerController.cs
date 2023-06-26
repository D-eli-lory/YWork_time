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
    public class YAnswerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public YAnswerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select AnswerID,TaskName,convert(varchar(10),Date_work,120) as Date_work, Kol_time,Comments
                            from dbo.YAnswer";
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
        public JsonResult Post(YAnswer ya)
        {
            string query = @" insert into dbo.YAnswer
                            (TaskName, Date_work, Kol_time, Comments)
                            values(@TaskName, @Date_work, @Kol_time, @Comments)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TaskName", ya.TaskName);
                    myCommand.Parameters.AddWithValue("@Date_work", ya.Date_work);
                    myCommand.Parameters.AddWithValue("@Kol_time", ya.Kol_time);
                    myCommand.Parameters.AddWithValue("@Comments", ya.Comments);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(YAnswer ya)
        {
            string query = @"update dbo.YAnswer
                            set TaskName=@TaskName, Date_work=@Date_work, Kol_time=@Kol_time, Comments=@Comments
                            where AnswerID=@AnswerID";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AnswerID", ya.AnswerID);
                    myCommand.Parameters.AddWithValue("@TaskName", ya.TaskName);
                    myCommand.Parameters.AddWithValue("@Date_work", ya.Date_work);
                    myCommand.Parameters.AddWithValue("@Kol_time", ya.Kol_time);
                    myCommand.Parameters.AddWithValue("@Comments", ya.Comments);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.YAnswer
                             where AnswerID=@AnswerID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AnswerID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
