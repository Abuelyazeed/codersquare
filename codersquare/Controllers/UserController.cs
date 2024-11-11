using codersquare.BL;
using codersquare.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        #region CreateUser
        //POST /api/user
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserCreateDto user)
        {
            await _userManager.CreateUser(user);
            return Ok("User created successfully");
        }

        #endregion

        #region GetUserByUsername
        //GET /api/user/username/ahmed715
        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserReadDto>> GetUserByUsername(string username)
        {
            UserReadDto user = await _userManager.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound($"User with username '{username}' not found.");
            }
            return Ok(user);
        }

        #endregion
        
        #region GetUserById
        //GET /api/user/id/{id}
        [HttpGet("id/{id:guid}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(Guid id)
        {
            UserReadDto user = await _userManager.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User not found.");
            }
            return Ok(user);
        }

        #endregion
        
        #region GetUserByEmail
        //GET /api/user/email/ahmed@mail.com
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserReadDto>> GetUserByEmail(string email)
        {
            UserReadDto user = await _userManager.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound($"User with email '{email}' not found.");
            }
            return Ok(user);
        }

        #endregion
        
        #region UpdateUser
        //GET /api/user/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateUser(UserUpdateDto user, Guid id)
        {
            bool isSuccess = await _userManager.UpdateUser(user, id);
            if(!isSuccess) return NotFound($"User with id {id} not found.");
            return Ok("User updated successfully.");
        }

        #endregion
    }
}
