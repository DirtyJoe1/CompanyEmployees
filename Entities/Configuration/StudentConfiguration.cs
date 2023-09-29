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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
            (
            new Student
            {
                Id = new Guid("49258e36-100f-4bdb-9cd0-cb5db876e903"),
                Name = "Pivkin Aleksey",
                Age = 18,
                GradeId = new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"),
            },
            new Student
            {
                Id = new Guid("cf757914-e4ef-4515-a5c2-ec0b3744ffc2"),
                Name = "Ovtin Ruslan",
                Age = 19,
                GradeId = new Guid("a3fb2ef3-a073-4ecd-9028-94b5305fe44c"),
            },
            new Student
            {
                Id = new Guid("ac963d4c-0383-4f52-9a64-2132be68b39b"),
                Name = "Kanaykin Aleksandr",
                Age = 18,
                GradeId = new Guid("a773b090-7d69-4a78-9a66-0e8eedb78ec5"),
            }
            );
        }
    }
}
