using DataAccess;
using Domain;

namespace Services
{
    public class StudentRegistrationService
    {
        public void RegisterNewStudent(Student student)
        {
            new StudentRepository().Save(student);
        }
    }
}