using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents(Guid gradeId, bool trackChanges);
        Student GetStudent(Guid gradeId, Guid id, bool trackChanges);
        void CreateStudentForGrade(Guid gradeId, Student student);
        void DeleteStudent(Student student);

    }
}
