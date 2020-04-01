using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw4.DataReceiver.Requests;
using cw4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw4.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private const string s = "Data Source=db-mssql;Initial Catalog=s19155;Integrated Security=True";
        [HttpPost]
        public IActionResult Enrollstudent(EnrollStudRequest esr)
        {
            var st = new Student();
            st.IndexNumber = esr.IndexNumber;
            st.FirstName = esr.FirstName;
            st.LastName = esr.LastName;
            st.BirthDate = esr.BirthDate;
            st.Studies = esr.Studies;

            using (SqlConnection sc = new SqlConnection(s))
            using (SqlCommand sq = new SqlCommand())
            {
                sq.Connection = sc;
                sc.Open();
                var tran = sc.BeginTransaction();

                sq.CommandText = "select idstudy from Studies where name = @name";
                sq.Parameters.AddWithValue("name", esr.Studies);
                var sdd = sq.ExecuteReader();
                if (!sdd.Read())
                {
                    return BadRequest("Studia nie istnieja");
                }

                sq.CommandText = "select 1 from Student where indexnumber = @indexnumber";
                sq.Parameters.AddWithValue("indexnumber", esr.IndexNumber);
                sdd = sq.ExecuteReader();
                if (!sdd.Read())
                {
                    return BadRequest("Index nie jest unikatowy");
                }
                int IdStudy = (int)sdd["IdStudy"];

                sq.CommandText = "select max(IdEnrollment) from Enrollment;";
                sdd = sq.ExecuteReader();
                int idEnrl = (int)sdd["IdEnrollment"];
                DateTime sdate = DateTime.Today;
                try
                {
                    sq.CommandText = "insert into Enrollment (IdEnrollment, Semester, IdStudy, StartDate) " +
                    "values(" + idEnrl + ", 1, " + IdStudy + ", " +
                    "" + sdate.ToShortDateString().Replace('.', '-').Reverse() + ");";
                    sq.ExecuteNonQuery();
                    tran.Commit();

                }
                catch (SqlException se)
                {
                    tran.Rollback();
                    return BadRequest("Cos poszlo zle");
                }
                return Ok();
            }
        }
    }
}