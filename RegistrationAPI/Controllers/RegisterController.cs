using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly DbRegisterStore _dbRegisterStore;
        private readonly IConfiguration _configuration;

        public RegisterController(DbRegisterStore dbRegisterStore, IConfiguration configuration)
        {
            _dbRegisterStore = dbRegisterStore;
            _configuration = configuration;
        }

        [HttpGet("/clients/all")]
        public IActionResult GetAllClients()
        {
            return Ok(_dbRegisterStore.GetAllClients());
        }

        [HttpPost("/clients/register")]
        public IActionResult RegisterClient(ClientRequest clientRequest)
        {
            _dbRegisterStore.RegisterClient(clientRequest);
            
            return Ok(new StatusModel
            {
                Status = "success"
            });
        }

        [HttpPost("/clients/authentication")]
        public IActionResult Authentication(AuthRequest authRequest)
        {
            var client = _dbRegisterStore.GetAllClients().FirstOrDefault(x =>
                x.Email == authRequest.Email && x.Password == authRequest.Password.GetHashCode().ToString());
            
            if (client == null)
                return Ok(new StatusModel{Status = "Wrong password or email."});

            var token = _configuration.GenerateJwtToken(client);
            
            return Ok(new StatusModel {Status = token});
            
        }
    }
}