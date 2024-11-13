using codersquare.BL;
using codersquare.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        #region SignUp
        //POST /api/signup
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] UserCreateDto user)
        {
            try
            {
                await _userManager.SignUp(user); 
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                // Return a 403 Forbidden response for this error
                return StatusCode(403, new { message = ex.Message });
            }
        }

        #endregion

        #region SignIn
        //POST /api/user/signin
        [HttpPost("signin")]
        public async Task<ActionResult> Sign([FromBody] UserSignInDto userSignInDto)
        {
            try
            {
                await _userManager.SignIn(userSignInDto); 
                return Ok("Signed in successfully.");
            }
            catch (Exception ex)
            {
                // Return a 403 Forbidden response for this error
                return StatusCode(403, new { message = ex.Message });
            }
        }

        #endregion
        
        #region UpdateUser
        //PATCH /api/users/{id}
        //Todo get current user
        [HttpPatch("users/{id:guid}")]
        public async Task<ActionResult> UpdateUser(UserUpdateDto user, Guid id)
        {
            bool isSuccess = await _userManager.UpdateUser(user, id);
            if(!isSuccess) return NotFound($"User with id {id} not found.");
            return Ok("User updated successfully.");
        }

        #endregion

        #region GetUserById
        //GET /api/users/{id}
        [HttpGet("users/{id:guid}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(Guid id)
        {
            UserReadDto user = await _userManager.GetUserById(id);
            if(user == null) return NotFound();
            return Ok(user);
        }

        #endregion
        
        //TODO
        //Get current user GET /api/users/
        
    }
}
