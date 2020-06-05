using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exemplo.Clientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {   
        private readonly ILogger<WeatherForecastController> _logger;

        public ClientesController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Clientes()
        {
            return new string[] { "Thiago Nishio", "Miguel Nishio" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Thiago Nishio - {id}";
        }
    }
}
