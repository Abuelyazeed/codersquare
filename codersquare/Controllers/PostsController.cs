using System.Security.Claims;
using codersquare.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostManager _postManager;

        public PostsController(IPostManager postManager)
        {
            _postManager = postManager;
        }
        
        #region CreatePost
        //POST /api/posts
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] PostCreateDto post)
        {
            // Get the current user's ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) 
            {
                return Unauthorized(new { message = "User ID not found in token. Please provide a valid token." });
            }
            
            await _postManager.CreatePost(post, userIdClaim.Value);
            return Ok("Post created successfully.");
        }
        #endregion
        
        #region DeletePost
        //DELETE /api/posts/{id}
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeletePost(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token. Please provide a valid token." });
            }

            string userId = userIdClaim.Value;
            
            
            bool isSuccess = await _postManager.DeletePost(id, userId);
            if (!isSuccess) 
            {
                return NotFound(new { message = $"Post not found or you are not authorized to delete it." });
            }
            
            return Ok("Post deleted successfully.");
        }

        #endregion

        #region GetAllPosts
        //GET /api/posts
        [HttpGet]
        public async Task<ActionResult<List<PostReadDto>>> GetAllPosts()
        {
            //throw new Exception("Test exception to verify error handling middleware");
            
            List<PostReadDto> posts = await _postManager.GetAllPosts();
            if (posts.Count == 0) return NotFound("No posts found.");

            return Ok(posts);
        }
        #endregion

        #region GetPostById
        //GET /api/posts/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostReadDto>> GetPostById(Guid id)
        {
            PostReadDto? post = await _postManager.GetPostById(id);
            if(post == null) return NotFound();
            
            return Ok(post);
        }

        #endregion
        
    }
}
