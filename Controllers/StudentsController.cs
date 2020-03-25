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
                sq.CommandText = "select s.IndexNumber, s.FirstName, s.LastName, e.Semester, ss.Name " +
                    "from student s, Enrollment e, Studies ss " +
                    "where s.IdEnrollment = e.IdEnrollment and e.IdStudy = ss.IdStudy;";
                sc.Open();

                SqlDataReader sd = sq.ExecuteReader();
                while (sd.Read())
                {

                    var st = new Student();
                    st.IndexNumber = sd["IndexNumber"].ToString();
                    st.FirstName = sd["FirstName"].ToString();
                    st.LastName = sd["LastName"].ToString();
                    st.Semestr = sd["Semester"].ToString();
                    st.Studia = sd["Name"].ToString();
                    list.Add(st);
                }
            }
            return Ok(list);
        }

        [HttpGet("{IndexNumber}")]
        public IActionResult GetStudent(string IndexNumber)
        {
            using (SqlConnection sc = new SqlConnection(s))
            using (SqlCommand sq = new SqlCommand())
            {
                sq.Connection = sc;
                sq.CommandText = "select s.IndexNumber, s.FirstName, s.LastName, e.Semester, ss.Name " +
                    "from student s, Enrollment e, Studies ss " +
                    "where s.IdEnrollment = e.IdEnrollment and e.IdStudy = ss.IdStudy " +
                    "and s.IndexNumber = '" + IndexNumber + "'";

                sc.Open();
                var sdd = sq.ExecuteReader();
                if (sdd.Read())
                {
                    var st = new Student();
                    st.IndexNumber = sdd["IndexNumber"].ToString();
                    st.FirstName = sdd["FirstName"].ToString();
                    st.LastName = sdd["LastName"].ToString();
                    st.Semestr = sdd["Semester"].ToString();
                    st.Studia = sdd["Name"].ToString();
                    return Ok(st);
                }
            }
            return NotFound();
        }
    }
}