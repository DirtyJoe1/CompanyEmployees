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
    public class GradeRepository : RepositoryBase<Grade>, IGradeRepository
    {
        public GradeRepository(RepositoryContext repositoryContext): base(repositoryContext) {}
        public async Task<IEnumerable<Grade>> GetAllGradesAsync(bool trackChanges) => await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
        public async Task<Grade> GetGradeAsync(Guid gradeId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(gradeId), trackChanges)
            .SingleOrDefaultAsync();
        public void CreateGrade(Grade grade) => Create(grade);
        public async Task<IEnumerable<Grade>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
        public void DeleteGrade(Grade grade)
        {
            Delete(grade);
        }
    }
}
