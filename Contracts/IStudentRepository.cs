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
        Task<IEnumerable<Student>> GetStudentsAsync(Guid gradeId, bool trackChanges);
        Task<Student> GetStudentAsync(Guid gradeId, Guid id, bool trackChanges);
        void CreateStudentForGrade(Guid gradeId, Student student);
        void DeleteStudent(Student student);

    }
}
