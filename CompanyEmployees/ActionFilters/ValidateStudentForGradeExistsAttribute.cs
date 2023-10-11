using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyEmployees.ActionFilters
{
    public class ValidateStudentForGradeExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateStudentForGradeExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var gradeId = (Guid)context.ActionArguments["gradeId"];
            var grade = await _repository.Grade.GetGradeAsync(gradeId, false);
            if (grade == null)
            {
                _logger.LogInfo($"Grade with id: {gradeId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var student = await _repository.Student.GetStudentAsync(gradeId, id, trackChanges);
            if (student == null)
            {
                _logger.LogInfo($"Student with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("student", student);
                await next();
            }
        }
    }
}
