using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public GradesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetGrades()
        {
            var grades = _repository.Grade.GetAllGrades(trackChanges: false);
            var gradesDto = _mapper.Map<IEnumerable<GradeDto>>(grades);
            return Ok(gradesDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetGrade(Guid id)
        {
            var grade = _repository.Grade.GetGrade(id, trackChanges: false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var gradeDto = _mapper.Map<GradeDto>(grade);
                return Ok(gradeDto);
            }
        }
    }
}
