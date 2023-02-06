using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository,ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task <IActionResult> LoginAsync(LoginRequest loginRequest)
        {
          var user= await userRepository.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);
            if (user != null) {
                tokenHandler.CreateTokenAsync(user);
                return Ok(user);
                //generate jwt
            }
            return BadRequest("User Name Or Password Is Invalid");
        }
    }
}
