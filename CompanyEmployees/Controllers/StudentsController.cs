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
        public async Task<IActionResult> GetStudentsForGrade(Guid gradeId)
        {
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentsFromDb = await _repository.Student.GetStudentsAsync(gradeId, trackChanges: false);
            var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(studentsFromDb);
            return Ok(studentsDto);
        }
        [HttpGet("{id}", Name = "GetStudentForGrade")]
        public async Task<IActionResult> GetStudentForGrade(Guid gradeId, Guid id)
        {
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentDb = await _repository.Student.GetStudentAsync(gradeId, id, trackChanges: false);
            if (studentDb == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var student = _mapper.Map<StudentDto>(studentDb);
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudentForGrade(Guid gradeId, [FromBody] StudentForCreationDto student)
        {
            if (student == null)
            {
                _logger.LogError("StudentForCreationDto object sent from client is null.");
                return BadRequest("StudentForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the StudentForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentEntity = _mapper.Map<Student>(student);
            _repository.Student.CreateStudentForGrade(gradeId, studentEntity);
            await _repository.SaveAsync();
            var studentToReturn = _mapper.Map<StudentDto>(studentEntity);
            return CreatedAtRoute("GetStudentForGrade", new
            {
                gradeId,
                id = studentToReturn.Id
            }, studentToReturn);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentForGrade(Guid gradeId, Guid id)
        {
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
            return NotFound();
            }
            var studentForGrade = await _repository.Student.GetStudentAsync(gradeId, id, trackChanges: false);
            if (studentForGrade == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _repository.Student.DeleteStudent(studentForGrade);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentForGrade(Guid gradeId, Guid id, [FromBody] StudentForUpdateDto student)
        {
            if (student == null)
            {
                _logger.LogError("StudentForUpdateDto object sent from client is null.");
            return BadRequest("StudentForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
            return NotFound();
            }
            var studentEntity = await _repository.Student.GetStudentAsync(gradeId, id, trackChanges: true);
            if (studentEntity == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _mapper.Map(student, studentEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateStudentForGrade(Guid gradeId, Guid id, [FromBody] JsonPatchDocument<StudentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentEntity = await _repository.Student.GetStudentAsync(gradeId, id,trackChanges: true);
            if (studentEntity == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var studentToPatch = _mapper.Map<StudentForUpdateDto>(studentEntity);
            patchDoc.ApplyTo(studentToPatch, ModelState);
            TryValidateModel(studentToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(studentToPatch, studentEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
