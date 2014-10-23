using System;
using System.Collections.Generic;
using DataAccess;
using Domain;
using NUnit.Framework;
using Rhino.Mocks;
using Services;
using StructureMap.AutoMocking;

namespace ServicesTests
{
    [TestFixture]
    public class StudentRegistrationServiceTests
    {
        [Test]
        public void RegisterNewStudent_SavesTheStudent_WhenTheStudentIsValid()
        {
            //Arrange
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();

            mockStudentValidator.Stub(x => x.ValidateStudent(Arg<Student>.Is.Anything)).Return(true);

            var studentRegistrationService = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

            const int studentId = 123;
            const string firstName = "John";
            const string lastName = "Doe";

            //Act -> call method being tested
            studentRegistrationService.RegisterNewStudent(studentId, firstName, lastName);

            //Assert
            mockStudentRepository.AssertWasCalled(x => x.Save(Arg<Student>.Matches(IsCustomerMatching(studentId, firstName, lastName))));
        }

        private static System.Linq.Expressions.Expression<Predicate<Student>> IsCustomerMatching(int studentId, string firstName, string lastName)
        {
            return y =>
                                    y.Id == studentId
                                    && y.FirstName == firstName
                                    && y.LastName == lastName;
        }
        //[Test]
        //public void RegisterNewStudent_ThrowsAnException_WhenTheStudentIsNotValid()
        //{
        //    //Arrange
        //    var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
        //    var mockStudentValidator = MockRepository.GenerateMock<IStudentValidator>();
        //    var studentRegistrationService = new StudentRegistrationService(mockStudentRepository, mockStudentValidator);

        //    try
        //    {
        //        //Act
        //        studentRegistrationService.RegisterNewStudent(null);

        //        //Assert
        //        Assert.Fail("Argument Exception expected");
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.IsInstanceOf<ArgumentException>(e);
        //    }
        //}

        //[Test]
        //public void RegisterNewStudent_SavesTheStudentWithPresenter()
        //{
        //    IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
        //    IStudentView mockStudentView = MockRepository.GenerateMock<IStudentView>();

        //    StudentRegistrationPresenter studentRegistrationService = new StudentRegistrationPresenter(mockStudentView,mockStudentRepository);

        //    Student student = new Student();
        //    student.Id = 123;
        //    student.FirstName = "John";
        //    student.LastName = "Doe";

        //    studentRegistrationService.RegisterNewStudent(student);
        //    mockStudentRepository.AssertWasCalled(x => x.Save(student));
        //}

        //[Test]
        //public void RegisterNewStudent_SetWasStudentSavedToTrueOnTheView_WhenTheStudentIsValid()
        //{
        //    IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
        //    IStudentView mockStudentView = MockRepository.GenerateMock<IStudentView>();

        //    StudentRegistrationPresenter studentRegistrationService = new StudentRegistrationPresenter(mockStudentView, mockStudentRepository);

        //    Student student = new Student();
        //    student.Id = 123;
        //    student.FirstName = "John";
        //    student.LastName = "Doe";

        //    studentRegistrationService.RegisterNewStudent(student);

        //    mockStudentView.AssertWasCalled(x => x.WasStudentSaved=true);
        //}
        [Test]
        public void RegisterNewStudent_SetWasStudentSavedToTrueOnTheView_WhenTheStudentIsNotValid()
        {
            IStudentRepository mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            IStudentView mockStudentView = MockRepository.GenerateMock<IStudentView>();

            StudentRegistrationPresenter studentRegistrationService = new StudentRegistrationPresenter(mockStudentView, mockStudentRepository);

            studentRegistrationService.RegisterNewStudent(null);

            mockStudentView.AssertWasCalled(x => x.WasStudentSaved = false);
        }
        [Test]
        public void Test_Equality()
        {
            var student1 = new Student();
            student1.Id=1;
            student1.FirstName="bob";
            student1.LastName="smith";

            var student2=student1;

            var student3 = new Student();
            student3.Id = 1;
            student3.FirstName = "bob";
            student3.LastName = "smith";

            Assert.AreEqual(student1, student2);
       //     Assert.AreEqual(student1, student3); //this will fail bcse of ref equlity being used

        }

        [Test]
        public void DeleteStudents_DeletesAllTheStudentsPassedIn()
        {
            //Arrange
            const int studentId1 = 123;
            const int studentId2 = 456;
            const int studentId3 = 789;

            Student student1 = new Student { Id = studentId1, FirstName = "FirstName", LastName = "LastName" };
            Student student2 = new Student { Id = studentId2, FirstName = "FirstName", LastName = "LastName" };
            Student student3 = new Student { Id = studentId3, FirstName = "FirstName", LastName = "LastName" };

            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            mockStudentRepository.Stub(x => x.FindById(studentId1)).Return(student1);
            mockStudentRepository.Stub(x => x.FindById(studentId2)).Return(student2);
            mockStudentRepository.Stub(x => x.FindById(studentId3)).Return(student3);

            var studentRegistrationService = new StudentRegistrationService(mockStudentRepository, null);

            //Act
            studentRegistrationService.DeleteStudents(studentId1, studentId2, studentId3);

            //Assert
            mockStudentRepository.AssertWasCalled(x => x.DeleteStudents(Arg<List<Student>>.List.ContainsAll(new List<Student> { student1, student2, student3 })));
            mockStudentRepository.AssertWasCalled(x => x.DeleteStudents(Arg<List<Student>>.List.Equal(new List<Student> { student1, student2, student3 })));
            mockStudentRepository.AssertWasCalled(x => x.DeleteStudents(Arg<List<Student>>.List.Count(Rhino.Mocks.Constraints.Is.Equal(3))));
            mockStudentRepository.AssertWasCalled(x => x.DeleteStudents(Arg<List<Student>>.List.IsIn(student1)));

            //All 4 valid assertions, but ContainsAll and Equal arent helpfull in this case?
            //isin is checking that student1 was passed into DeleteStudents method
        }

        [Test]
        public void GetStudentsByFirstName_GetTheStudentFromTheRepository()
        {
            //text contraints

            //Arrange
            var mockStudentRepository = MockRepository.GenerateMock<IStudentRepository>();
            var studentRegistrationService = new StudentRegistrationService(mockStudentRepository, null);

            //Act
            studentRegistrationService.GetStudentByFirstName("Jones");

            //Assert
            mockStudentRepository.AssertWasCalled(x => x.FindByLastName(Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Contains("Jones"))));
            mockStudentRepository.AssertWasCalled(x => x.FindByLastName(Arg<string>.Matches(Rhino.Mocks.Constraints.Text.EndsWith("nes"))));
            mockStudentRepository.AssertWasCalled(x => x.FindByLastName(Arg<string>.Matches(Rhino.Mocks.Constraints.Text.StartsWith("Jon"))));
            mockStudentRepository.AssertWasCalled(x => x.FindByLastName(Arg<string>.Matches(Rhino.Mocks.Constraints.Text.Like("Jone.*")))); //regex
        }

        [Test]
        public void RegisterNewStudent_SavesTheStudent_WhenTheStudentIsValid_WithOutParam()
        {
            //Arrange
            //needed structuremap.automocking nuget pack
            var autoMocker = new RhinoAutoMocker<StudentRegistrationService>();

            var mockStudentValidator = autoMocker.Get<IStudentValidator>();
            //syntax for out param
            //mockStudentValidator.Stub(x => x.ValidateStudent(Arg<Student>.Out(new Student()).Dummy)).Return(true);

            //for out
            // mockStudentValidator.Stub(x => x.ValidateStudent(Arg<Student>.Is.Anything, out Arg<Student>.Out(new Student()).Dummy)).Return(true);
            //for ref
            mockStudentValidator.Stub(x => x.ValidateStudent(
                Arg<Student>.Is.Anything, 
                ref Arg<Student>.Ref(
                    Rhino.Mocks.Constraints.Is.Anything(), new Student()).Dummy
                )
            ).Return(true);
            
            const int studentId = 123;
            const string firstName = "John";
            const string lastName = "Doe";

            //Act
            autoMocker.ClassUnderTest.RegisterNewStudent(studentId, firstName, lastName);

            //Assert
            autoMocker.Get<IStudentRepository>().AssertWasCalled(x => x.Save(Arg<Student>.Matches(y => y.IsCustomerMatching(firstName,lastName,studentId,y))));
        }
    }
}
