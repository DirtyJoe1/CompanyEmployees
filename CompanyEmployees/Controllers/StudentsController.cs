using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/grades/{gradeId}/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public StudentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetStudentsForGrade(Guid gradeId)
        {
            var grade = _repository.Grade.GetGrade(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentsFromDb = _repository.Student.GetStudents(gradeId, trackChanges: false);
            var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(studentsFromDb);
            return Ok(studentsDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudentForGrade(Guid gradeId, Guid id)
        {
            var grade = _repository.Grade.GetGrade(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentDb = _repository.Student.GetStudent(gradeId, id, trackChanges: false);
            if (studentDb == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var student = _mapper.Map<StudentDto>(studentDb);
            return Ok(student);
        }
    }
}
