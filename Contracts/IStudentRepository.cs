using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStudentRepository
    {
        Task<PagedList<Student>> GetStudentsAsync(Guid gradeId, StudentParameters studentParameters, bool trackChanges);
        Task<Student> GetStudentAsync(Guid gradeId, Guid id, bool trackChanges);
        void CreateStudentForGrade(Guid gradeId, Student student);
        void DeleteStudent(Student student);

    }
}
