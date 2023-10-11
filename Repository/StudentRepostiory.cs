using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepostiory : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepostiory(RepositoryContext repositoryContext): base(repositoryContext) { }
        public async Task<IEnumerable<Student>> GetStudentsAsync(Guid gradeId, bool trackChanges) => await FindByCondition(e => e.GradeId.Equals(gradeId), trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();
        public async Task<Student> GetStudentAsync(Guid gradeId, Guid id, bool trackChanges) => await FindByCondition(e => e.GradeId.Equals(gradeId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
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
