using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{id}", Name = "GetStudentForGrade")]
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
        [HttpPost]
        public IActionResult CreateStudentForGrade(Guid gradeId, [FromBody] StudentForCreationDto student)
        {
            if (student == null)
            {
                _logger.LogError("StudentForCreationDto object sent from client is null.");
                return BadRequest("StudentForCreationDto object is null");
            }
            var grade = _repository.Grade.GetGrade(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentEntity = _mapper.Map<Student>(student);
            _repository.Student.CreateStudentForGrade(gradeId, studentEntity);
            _repository.Save();
            var studentToReturn = _mapper.Map<StudentDto>(studentEntity);
            return CreatedAtRoute("GetStudentForGrade", new
            {
                gradeId,
                id = studentToReturn.Id
            }, studentToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudentForGrade(Guid gradeId, Guid id)
        {
            var grade = _repository.Grade.GetGrade(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
            return NotFound();
            }
            var studentForGrade = _repository.Student.GetStudent(gradeId, id, trackChanges: false);
            if (studentForGrade == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _repository.Student.DeleteStudent(studentForGrade);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudentForGrade(Guid gradeId, Guid id, [FromBody] StudentForUpdateDto student)
        {
            if (student == null)
            {
                _logger.LogError("StudentForUpdateDto object sent from client is null.");
            return BadRequest("StudentForUpdateDto object is null");
            }
            var grade = _repository.Grade.GetGrade(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
            return NotFound();
            }
            var studentEntity = _repository.Student.GetStudent(gradeId, id, trackChanges: true);
            if (studentEntity == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _mapper.Map(student, studentEntity);
            _repository.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateStudentForGrade(Guid gradeId, Guid id, [FromBody] JsonPatchDocument<StudentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var grade = _repository.Grade.GetGrade(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentEntity = _repository.Student.GetStudent(gradeId, id,trackChanges: true);
            if (studentEntity == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var studentToPatch = _mapper.Map<StudentForUpdateDto>(studentEntity);
            patchDoc.ApplyTo(studentToPatch);
            _mapper.Map(studentToPatch, studentEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
