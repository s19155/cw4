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
//        insert into Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment)
//values('s19155', 'Olena', 'Lukaskchuk', '2001-01-16', 1),
//		('s18456', 'Jan', 'Baka', '2000-08-19', 2),
//		('s19105', 'Marcin', 'Polak', '1998-12-04', 3);

//insert into Enrollment(IdEnrollment, Semester, IdStudy, StartDate)
//values(1, 2, 1, '2018-10-12'),
//		(2, 2, 1, '2018-10-16'),
//		(3, 1, 1, '2019-10-12');

//insert into Studies(IdStudy, Name)
//values(1, 'Informatyka'),
//		(2, 'Architektura wnetrz'),
//		(3, 'Sztuka Nowych Mediow'),
//		(4, 'Kultura Japonii'),
//		(5, 'Zarzadzanie informacja');

//select s.IndexNumber, s.FirstName, s.LastName, e.Semester, ss.Name
//from student s, Enrollment e, Studies ss
//where s.IdEnrollment = e.IdEnrollment and e.IdStudy = ss.IdStudy
//and s.IndexNumber like 's19155';

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
                    "and s.IndexNumber = @index";
                sq.Parameters.AddWithValue("index", IndexNumber);
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