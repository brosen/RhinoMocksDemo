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