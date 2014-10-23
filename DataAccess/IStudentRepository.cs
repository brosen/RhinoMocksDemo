using System;
using System.Collections.Generic;
using Domain;
namespace DataAccess
{
    public interface IStudentRepository
    {
        Student FindById(int id);
        void Save(Student student);
        void DeleteStudents(List<Student> students);
        void DeleteStudent(Student student);

        Student FindByLastName(string p);
    }
}
