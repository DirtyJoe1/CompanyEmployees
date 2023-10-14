using AutoMapper;
using CompanyEmployees.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.DataShaping;

namespace CompanyEmployees.Controllers
{
    [Route("api/grades/{gradeId}/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<StudentDto> _dataShaper;
        public StudentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<StudentDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentsForGrade(Guid gradeId, [FromQuery] StudentParameters studentParameters)
        {
            if (!studentParameters.ValidAgeRange)
                return BadRequest("Max age can't be less than min age.");
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentsFromDb = await _repository.Student.GetStudentsAsync(gradeId, studentParameters,trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(studentsFromDb.MetaData));
            var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(studentsFromDb);
            return Ok(_dataShaper.ShapeData(studentsDto, studentParameters.Fields));
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateStudentForGrade(Guid gradeId, [FromBody] StudentForCreationDto student)
        {
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
        [ServiceFilter(typeof(ValidateStudentForGradeExistsAttribute))]
        public async Task<IActionResult> DeleteStudentForGrade(Guid gradeId, Guid id)
        {
            var studentForGrade = HttpContext.Items["student"] as Student;
            _repository.Student.DeleteStudent(studentForGrade);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateStudentForGradeExistsAttribute))]
        public async Task<IActionResult> UpdateStudentForGrade(Guid gradeId, Guid id, [FromBody] StudentForUpdateDto student)
        {
            var studentEntity = HttpContext.Items["student"] as Student;
            _mapper.Map(student, studentEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateStudentForGradeExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateStudentForGrade(Guid gradeId, Guid id, [FromBody] JsonPatchDocument<StudentForUpdateDto> patchDoc)
        {
            var grade = await _repository.Grade.GetGradeAsync(gradeId, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return NotFound();
            }
            var studentEntity = HttpContext.Items["student"] as Student;
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
