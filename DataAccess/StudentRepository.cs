using Domain;
using LowLevelDataLayer;

namespace DataAccess
{
    public class StudentRepository : IStudentRepository 
    {
        public Student FindById(int id)
        {
            DataCommand someLowLevelDataCommand = new DataCommand();
            var result = someLowLevelDataCommand.ExecuteReader("Select * From Students Where...");
            
            Student student = new Student();
            student.Id = (int)result["Id"];
            student.FirstName = (string)result["FirstName"];
            student.LastName = (string)result["LastName"];

            return student;
        }

        public void Save(Student student)
        {
            DataCommand someLowLevelDataCommand = new DataCommand();
            //just to simulate database command
            someLowLevelDataCommand.ExecuteCommand("Insert into Students(Id, FirstName, LastName)...");
        }
    }
}