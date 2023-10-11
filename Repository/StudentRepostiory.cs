using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepostiory : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepostiory(RepositoryContext repositoryContext)
            : base(repositoryContext) { }
        public IEnumerable<Student> GetStudents(Guid gradeId, bool trackChanges) =>
            FindByCondition(e => e.GradeId.Equals(gradeId), trackChanges)
            .OrderBy(e => e.Name);
        public Student GetStudent(Guid gradeId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.GradeId.Equals(gradeId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();
        public void CreateStudentForGrade(Guid gradeId, Student student)
        {
            student.GradeId = gradeId;
            Create(student);
        }
        public void DeleteStudent(Student student)
        {
            Delete(student);
        }
    }
}
