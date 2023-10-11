using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetAllGradesAsync(bool trackChanges);
        Task<Grade> GetGradeAsync(Guid gradeId, bool trackChanges);
        void CreateGrade(Grade grade);
        Task<IEnumerable<Grade>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteGrade(Grade grade);
    }
}
