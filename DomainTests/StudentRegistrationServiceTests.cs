using System;
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
        public void RegisterNewStudent_SavesTheStudent_WhenTheStudentIsValid()
        {
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();

            mockStudentValidator.Stub(x => x.ValidateStudent(Arg<Student>.Is.Anything)).Return(true);

            var studentRegistrationService = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

            Student student = new Student();
            student.Id = 123;
            student.FirstName = "John";
            student.LastName = "Doe";

            studentRegistrationService.RegisterNewStudent(student);

            mockStudentRepository.AssertWasCalled(x => x.Save(student));
        }
        [Test]
        public void RegisterNewStudent_ThrowsAnException_WhenTheStudentIsNotValid()
        {
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();
            var studentRegistrationService = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

            try
            {
                studentRegistrationService.RegisterNewStudent(null);
                Assert.Fail("Argument Exception expected");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<ArgumentException>(e);
            }
        }

        [Test]
        public void RegisterNewStudent_SavesTheStudentWithPresenter()
        {
            IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            IStudentView mockStudentView = MockRepository.GenerateMock<IStudentView>();

            StudentRegistrationPresenter studentRegistrationService = new StudentRegistrationPresenter(mockStudentView,mockStudentRepository);

            Student student = new Student();
            student.Id = 123;
            student.FirstName = "John";
            student.LastName = "Doe";

            studentRegistrationService.RegisterNewStudent(student);
            mockStudentRepository.AssertWasCalled(x => x.Save(student));
        }

        [Test]
        public void RegisterNewStudent_SetWasStudentSavedToTrueOnTheView_WhenTheStudentIsValid()
        {
            IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            IStudentView mockStudentView = MockRepository.GenerateMock<IStudentView>();

            StudentRegistrationPresenter studentRegistrationService = new StudentRegistrationPresenter(mockStudentView, mockStudentRepository);

            Student student = new Student();
            student.Id = 123;
            student.FirstName = "John";
            student.LastName = "Doe";

            studentRegistrationService.RegisterNewStudent(student);

            mockStudentView.AssertWasCalled(x => x.WasStudentSaved=true);
        }
        [Test]
        public void RegisterNewStudent_SetWasStudentSavedToTrueOnTheView_WhenTheStudentIsNotValid()
        {
            IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            IStudentView mockStudentView = MockRepository.GenerateMock<IStudentView>();

            StudentRegistrationPresenter studentRegistrationService = new StudentRegistrationPresenter(mockStudentView, mockStudentRepository);

            studentRegistrationService.RegisterNewStudent(null);

            mockStudentView.AssertWasCalled(x => x.WasStudentSaved = false);
        }
    }
}
