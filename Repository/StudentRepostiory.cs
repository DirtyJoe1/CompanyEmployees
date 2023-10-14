using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepostiory : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepostiory(RepositoryContext repositoryContext): base(repositoryContext) { }
        public async Task<PagedList<Student>> GetStudentsAsync(Guid gradeId, StudentParameters studentParameters, bool trackChanges)
        {
            var students = await FindByCondition(e => e.GradeId.Equals(gradeId), trackChanges)
                .FilterStudents(studentParameters.MinAge, studentParameters.MaxAge)
                .Search(studentParameters.SearchTerm)
                .Sort(studentParameters.OrderBy)
                .ToListAsync();
            return PagedList<Student>
                .ToPagedList(students, studentParameters.PageNumber, studentParameters.PageSize);
        }


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
