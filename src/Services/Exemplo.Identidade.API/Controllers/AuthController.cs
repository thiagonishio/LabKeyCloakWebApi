using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exemplo.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/identidade")]
    public class AuthController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(LoginUsuarioViewModel loginUsuarioVM)
        {
            if (!ModelState.IsValid) return BadRequest();

            var values = new Dictionary<string, string>
            {
                { "client_id", _config["Jwt:ClientId"] },
                { "grant_type", _config["Jwt:GrantType"] },
                { "client_secret", _config["Jwt:Secret"] },
                { "username", loginUsuarioVM.Usuario },
                { "password", loginUsuarioVM.Senha }
            };

            var contentData = new FormUrlEncodedContent(values);


            var client = new HttpClient();

            var response = await client.PostAsync($"{_config["Jwt:Authority"]}/{_config["Jwt:Realm"]}/protocol/openid-connect/token", contentData);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseToken = JsonConvert.DeserializeObject<ResponseToken>(responseString);

            return Json(responseToken);
        }
    }
}
