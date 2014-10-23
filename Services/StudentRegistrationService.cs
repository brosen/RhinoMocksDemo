using System;
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
        public void RegisterNewStudent(Student student)
        {
            //returning rhinomocks mock, which is the mock, and when no implementation
            //it will always return false which will cause the RegisterNewStudent_SavesTheStudent_WhenTheStudentIsValid
            //test to fail -> need mockStudentValidator.Stub()
            bool isStudentValid = _studentValidator.ValidateStudent(student); 
            if (isStudentValid)
            {
                _studentRepository.Save(student);
            }
            else
            {
                throw new ArgumentException("Invalid Student","student");
            }
        }
    }
}