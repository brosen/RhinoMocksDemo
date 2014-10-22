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
            //returns an interface, which is not an instance of the StudentRepository class
            IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>(); 

            StudentRegistrationService studentRegistrationService = new StudentRegistrationService(mockStudentRepository);

            Student student = new Student();
            student.Id = 123;
            student.FirstName = "John";
            student.LastName = "Doe";

            studentRegistrationService.RegisterNewStudent(student);

            //no need to find student by id anymore
            //the AssertWasCalled goes to lamba expression and calls the Save on a IStudentRepository 
            //  and passes student
            mockStudentRepository.AssertWasCalled(x => x.Save(student));
        }
    }
}
