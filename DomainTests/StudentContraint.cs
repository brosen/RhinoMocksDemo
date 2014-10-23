using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Rhino.Mocks.Constraints;

namespace ServicesTests
{
    public class StudentContraint : AbstractConstraint 
    {
         readonly Student _expectedStudent;
         Student _actualStudent;

        public StudentContraint(Student expectedStudent)
        {
            _expectedStudent = expectedStudent;
        }

        public override bool Eval(object actual)
        {
            _actualStudent = actual as Student;

            if (_actualStudent == null && _expectedStudent == null)
                return true;

            if (_actualStudent == null || _expectedStudent == null)
                return false;

            return _actualStudent.Id == _expectedStudent.Id
                && _actualStudent.FirstName == _expectedStudent.FirstName
                && _actualStudent.LastName == _expectedStudent.LastName;
        }
        public override string Message
        {
            get 
            {
                var messageBuilder = new StringBuilder();
                if (_actualStudent.Id != _expectedStudent.Id)
                {
                    messageBuilder.AppendLine(string.Format("Expected Id {0}, Actual Id {1}",_actualStudent.Id, _expectedStudent.Id));
                }
                if (_actualStudent.FirstName != _expectedStudent.FirstName)
                {
                    messageBuilder.AppendLine(string.Format("Expected FirstName {0}, Actual FirstName {1}", _actualStudent.FirstName, _expectedStudent.FirstName));
                }

                if (_actualStudent.LastName != _expectedStudent.LastName)
                {
                    messageBuilder.AppendLine(string.Format("Expected LastName {0}, Actual LastName {1}", _actualStudent.LastName, _expectedStudent.LastName));
                }
                return messageBuilder.ToString();
            }
        }
    }
}
