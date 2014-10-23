using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Domain;

namespace Services
{
    public class StudentRegistrationPresenter
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentView _studentView;

        public StudentRegistrationPresenter(IStudentView studentView, IStudentRepository studentRepository)
        {
            _studentView = studentView;
            _studentRepository = studentRepository;
        }
        public void RegisterNewStudent(Student student)
        {
            if (_studentView.ShouldSaveStudent)
            {
                _studentRepository.Save(student);
                _studentView.WasStudentSaved = true;
            }
            else
            {
                _studentView.WasStudentSaved = false;
            }
        }
    }
}
