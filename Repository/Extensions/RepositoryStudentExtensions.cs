using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryStudentExtensions
    {
        public static IQueryable<Student> FilterStudents(this IQueryable<Student> students, uint minAge, uint maxAge) => students.Where(e => (e.Age >= minAge && e.Age <= maxAge));
        public static IQueryable<Student> Search(this IQueryable<Student> students, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return students;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return students.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Student> Sort(this IQueryable<Student> students, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return students.OrderBy(e => e.Name);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Student>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return students.OrderBy(e => e.Name);
            return students.OrderBy(orderQuery);
        }
    }
}
