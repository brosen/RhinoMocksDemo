namespace Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsCustomerMatching(string firstName, string lastName, int studentId, Student y)
        {
            //throw new System.NotImplementedException();
            if (studentId == Id)
                return true;
            else
                return false;

        }
    }
}