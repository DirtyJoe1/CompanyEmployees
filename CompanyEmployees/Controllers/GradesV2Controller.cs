using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/grades")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class GradesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public GradesV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var grades = await _repository.Grade.GetAllGradesAsync(trackChanges: false);
            return Ok(grades);
        }
    }
}
