using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IGradeRepository _gradeRepository;
        private IStudentRepository _studentRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public IGradeRepository Grade
        {
            get
            {
                if (_gradeRepository == null)
                    _gradeRepository = new GradeRepository(_repositoryContext);
                return _gradeRepository;
            }
        }
        public IStudentRepository Student
        {
            get
            {
                if (_studentRepository == null)
                    _studentRepository = new StudentRepostiory(_repositoryContext);
                return _studentRepository;
            } 
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}
