/// <summary>
/// The Login method handles user authentication by validating the provided credentials against predefined values. 
/// If the credentials match either the owner's or the customer's, a JWT token is generated and returned. 
/// Otherwise, an unauthorized response is returned. This method uses the LoginRequest class to capture the user's login details.
/// </summary>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
namespace RestaurantReservationSystem.Api.Controllers 
{
    [ApiController] 
    [Route("api/[controller]")] 
    [AllowAnonymous] 


    public class AuthController : ControllerBase 
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;  


        public AuthController(JwtTokenGenerator jwtTokenGenerator) 
        {
            _jwtTokenGenerator = jwtTokenGenerator; 
        }

        [HttpPost("login")] 
        public IActionResult Login([FromBody] LoginRequest request) 
        {
            
            if (request.Username == "owner" && request.Password == "Owner@123") 
            {
                var token = _jwtTokenGenerator.GenerateToken(request.Username, "RestaurantOwner"); 
                return Ok(new { Token = token }); 
            }

            if (request.Username == "customer" && request.Password == "Customer@123") 
            {
                var token = _jwtTokenGenerator.GenerateToken(request.Username, "Customer"); 
                return Ok(new { Token = token }); 
            }

            return Unauthorized("Invalid credentials"); 
        }
    }

    public class LoginRequest 
    {
        public string Username { get; set; } 
        public string Password { get; set; } 
    }
}
