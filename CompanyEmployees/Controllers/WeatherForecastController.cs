using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;
        public WeatherForecastController(ILoggerManager logger)
        {
            logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("¬от информационное сообщение от нашего контроллера значений.");


            _logger.LogDebug("¬от отладочное сообщение от нашего контроллера значений.");


            _logger.LogWarn("¬от сообщение предупреждени€ от нашего контроллера значений.");


            _logger.LogError("¬от сообщение об ошибке от нашего контроллера значений.");
        return new string[] { "value1", "value2" };
        }
    }
}