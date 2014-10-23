using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Domain;
using NUnit.Framework;
using Rhino.Mocks;
using Services;

namespace ServicesTests
{
    [TestFixture]
    public class StudentRegistrationPresenterTests
    {
        [Test]
        public void RegisterNewStudent_SavesTheStudent_WhenShouldSaveStudentIsSetToTrueOnTheView()
        {
            IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            IStudentView mockStudentView = MockRepository.GenerateMock<IStudentView>();
            mockStudentView.Stub(x => x.ShouldSaveStudent).Return(true);

            StudentRegistrationPresenter studentRegistrationService = new StudentRegistrationPresenter(mockStudentView, mockStudentRepository);

            Student student = new Student();
            student.Id = 123;
            student.FirstName = "John";
            student.LastName = "Doe";

            studentRegistrationService.RegisterNewStudent(student);
            mockStudentRepository.AssertWasCalled(x => x.Save(student));

        }
    }
}
