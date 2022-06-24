using Manager.API.Token;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ITokenGenerator tokenGenerator;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            this.configuration = configuration;
            this.tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("auth/login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var tokenLogin = configuration["Jwt:Login"];
                var tokenPassword = configuration["Jwt:Password"];
                if(loginViewModel.Login == tokenLogin && loginViewModel.Password == tokenPassword)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "usuário autenticado com sucesso!",
                        Success = true,
                        Data = new 
                        {
                            Token = tokenGenerator.GenerateToken(),
                            TokenExpires = DateTime.UtcNow.AddHours(int.Parse(configuration["Jwt:HoursToExpire"]))
                        }
                    });
                }
                else
                {
                    return StatusCode(401, Responses.UnauthorizedErrorMessage());
                }
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }    
        }
    }
}