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
    }
}
