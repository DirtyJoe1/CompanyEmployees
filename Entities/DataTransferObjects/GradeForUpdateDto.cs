using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class GradeForUpdateDto
    {
        public string Name { get; set; }
        public IEnumerable<StudentForCreationDto> Students { get; set; }
    }
}
