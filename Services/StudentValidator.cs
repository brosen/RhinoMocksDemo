using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Services
{
    public class StudentValidator : IStudentValidator
    {
        public bool ValidateStudent(Student student)
        {
            return student != null;
        }
       // public bool ValidateStudent(Student student, out Student outStudent)
        public bool ValidateStudent(Student student, ref Student outStudent)
        {
            throw new NotImplementedException();
        }
    }
}
