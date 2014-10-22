using DataAccess;
using Domain;
using NUnit.Framework;
using Rhino.Mocks;
using Services;

namespace ServicesTests
{
    [TestFixture]
    public class StudentRegistrationServiceTests
    {
        [Test]
        public void RegisterNewStudent_SavesTheStudent()
        {
            StudentRegistrationService studentRegistrationService = new StudentRegistrationService();

            Student student = new Student();
            student.Id = 123;
            student.FirstName = "John";
            student.LastName = "Doe";

            studentRegistrationService.RegisterNewStudent(student);

            StudentRepository studentRepository = new StudentRepository();
            Student savedStudent = studentRepository.FindById(123);

            Assert.AreEqual(student.Id, savedStudent.Id);
            Assert.AreEqual(student.FirstName, savedStudent.FirstName);
            Assert.AreEqual(student.LastName, savedStudent.LastName);
        }
    }
}
