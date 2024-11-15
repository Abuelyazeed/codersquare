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
        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] PostCreateDto post)
        {
            await _postManager.CreatePost(post);
            return Ok("Post created successfully.");
        }
        #endregion

        #region GetAllPosts
        //GET /api/posts
        [HttpGet]
        [Authorize]
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

        #region DeletePost
        //DELETE /api/posts/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeletePost(Guid id)
        {
            bool isSuccess = await _postManager.DeletePost(id);
            if(!isSuccess) return NotFound($"Post with id {id} not found.");
            
            return Ok("Post deleted successfully.");
        }

        #endregion
    }
}
