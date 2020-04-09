using cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Services
{
    public interface IdStudentDbService
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(string index);
    }
}
