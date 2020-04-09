using cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Services
{
    public class SqlServerStudentService : IdStudentDbService
    {

        public Student GetStudent(string index)
        {
            if(index == "s19105" || index == "s18456" || index == "s19155")
            {
                return new Student { IndexNumber = "s0000", FirstName = "Lan", LastName = "Li" };
            }
            return null;
        }

        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }
    }
}
