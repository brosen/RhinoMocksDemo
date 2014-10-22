using System;
namespace DataAccess
{
    interface IStudentRepository
    {
        Domain.Student FindById(int id);
        void Save(Domain.Student student);
    }
}
