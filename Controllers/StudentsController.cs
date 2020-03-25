using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw4.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw4.Controllers
{
    [ApiController]
    [Route ("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string s = "Data Source=db-mssql;Initial Catalog=s19155;Integrated Security=True";
        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();
            using (SqlConnection sc = new SqlConnection(s))
            using (SqlCommand sq = new SqlCommand())
            {
                sq.Connection = sc;
                sq.CommandText = "select * from student";
                sc.Open();

                SqlDataReader sd = sq.ExecuteReader();
                while (sd.Read())
                {

                    var st = new Student();
                    st.IndexNumber = sd["IndexNumber"].ToString();
                    st.FirstName = sd["FirstName"].ToString();
                    st.LastName = sd["LastName"].ToString();
                    list.Add(st);
                }
            }
            return Ok(list);
        }
    }
}