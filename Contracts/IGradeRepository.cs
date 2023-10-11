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
        IEnumerable<Grade> GetAllGrades(bool trackChanges);
        Grade GetGrade(Guid gradeId, bool trackChanges);
        void CreateGrade(Grade grade);
        IEnumerable<Grade> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteGrade(Grade grade);
    }
}
