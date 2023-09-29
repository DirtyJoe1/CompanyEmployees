using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasData
            (
            new Grade
            {
                Id = new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"),
                Name = "409A",
            },
            new Grade
            {
                Id = new Guid("a773b090-7d69-4a78-9a66-0e8eedb78ec5"),
                Name = "409B",
            }
            );
        }
    }
}
