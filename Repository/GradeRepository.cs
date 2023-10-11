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
    public class GradeRepository : RepositoryBase<Grade>, IGradeRepository
    {
        public GradeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) 
        {

        }
        public IEnumerable<Grade> GetAllGrades(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
        public Grade GetGrade(Guid gradeId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(gradeId), trackChanges)
            .SingleOrDefault();
        public void CreateGrade(Grade grade) => Create(grade);
        public IEnumerable<Grade> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        public void DeleteGrade(Grade grade)
        {
            Delete(grade);
        }
    }
}
