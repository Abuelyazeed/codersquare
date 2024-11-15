using System.Security.Claims;
using codersquare.BL;
using codersquare.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ITokenManager _tokenManager;

        public UsersController(IUserManager userManager, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        #region SignUp
        //POST /api/signup
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUp user)
        {
            try
            {
                await _userManager.SignUp(user); 
                return Ok("User created successfully.");
            }
            catch (ArgumentException ex)
            {
                //400 Error bad request
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        #endregion

        #region Login
        //POST /api/user/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                 // Sign in and get the UserReadDto object
                 var newUserDto = await _userManager.Login(loginDto);

                 if (newUserDto == null)
                 {
                     // Return 401 Unauthorized for invalid credentials
                     return Unauthorized(new { message = "Invalid username/email or password." });
                 }
        
                 return Ok(new { message = "Signed in successfully.",  newUserDto});
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { message = ex.Message });
            }
        }

        #endregion
        
        #region UpdateUser
        //PATCH /api/users
        [Authorize]
        [HttpPatch("users")]
        public async Task<ActionResult> UpdateUser(UserUpdateDto user)
        {
            // Get the current user's ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("User ID not found in token.");

            Guid userId = Guid.Parse(userIdClaim.Value);
            
            bool isSuccess = await _userManager.UpdateUser(user, userId);
            if(!isSuccess) return NotFound(new { message = $"User with ID {userId} not found or update failed." });
            return Ok("User updated successfully.");
        }

        #endregion

        #region GetUserById
        //GET /api/users/{id}
        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(string id)
        {
            UserReadDto user = await _userManager.GetUserById(id);
            if(user == null) return NotFound(new { message = "User not found." });
            return Ok(user);
        }

        #endregion
        
        #region GetCurrentUser
        // GET /api/users/
        [Authorize]
        [HttpGet("users/me")]
        public async Task<ActionResult<NewUserDto>> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized(new { message = "User ID not found in token. Please ensure you're logged in." });
            
            string userId = userIdClaim.Value;

            UserReadDto newUser = await _userManager.GetUserById(userId);
            if (newUser == null) return NotFound(new { message = "User not found." });
            return Ok(newUser);
        }
        #endregion
        
    }
}
