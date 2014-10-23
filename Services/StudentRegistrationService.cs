using System;
using System.Collections.Generic;
using DataAccess;
using Domain;

namespace Services
{
    public class StudentRegistrationService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentValidator _studentValidator;

        public StudentRegistrationService(IStudentRepository studentRepository, IStudentValidator studentValidator)
        {
            _studentRepository = studentRepository;
            _studentValidator = studentValidator;
        }
        //public void RegisterNewStudent(Student student)
        //{
        //    bool isStudentValid = _studentValidator.ValidateStudent(student); 
        //    if (isStudentValid)
        //    {
        //        _studentRepository.Save(student);
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Invalid Student","student");
        //    }
        //}
        public void RegisterNewStudent(int studentId, string firstName, string lastName)
        {
            var student = new Student { Id = studentId, FirstName = firstName, LastName = lastName };
            Student outStudent=null;
            bool isStudentValid = _studentValidator.ValidateStudent(student, ref outStudent);

            if (outStudent == null)
            {
                throw new Exception("outstudent was null");
            }

            if (isStudentValid)
            {
                _studentRepository.Save(student);
            }
            else
            {
                throw new ArgumentException("Invalid Student", "student");
            }
        }

        public void DeleteStudents(params int[] studentIds)
        {
            List<Student> students = new List<Student>();
            foreach (var studentId in studentIds)
            {
                students.Add(_studentRepository.FindById(studentId));
            }
            _studentRepository.DeleteStudents(students);
        }
        public void DeleteStudent(Student student)
        {
            _studentRepository.DeleteStudent(student);
        }

        public Student GetStudentByFirstName(string lastName)
        {
            return _studentRepository.FindByLastName(lastName);
        }
    }
}