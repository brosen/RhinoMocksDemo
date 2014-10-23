using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Services
{
    public interface IStudentValidator
    {
        bool ValidateStudent(Student student);
    }
}
