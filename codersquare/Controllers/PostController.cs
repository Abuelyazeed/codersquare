using codersquare.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostManager _postManager;

        public PostController(IPostManager postManager)
        {
            _postManager = postManager;
        }
        
        #region CreatePost
        //POST /api/post/new/{userId}
        [HttpPost("new/{userId:id}")]

        public async Task<ActionResult> CreatePost(Guid userId,[FromBody] PostCreateDto post)
        {
            await _postManager.CreatePost(post,userId);
            return Ok("Post created successfully.");
        }

        #region GetAllPosts
        //GET /api/post/list
        [HttpGet("list")]
        public async Task<ActionResult<List<PostReadDto>>> GetAllPosts()
        {
            List<PostReadDto> posts = await _postManager.GetAllPosts();
            if (posts == null || posts.Count == 0) return NotFound("No posts found.");

            return Ok(posts);
        }
        #endregion
        //GET /api/post/{id}
        [HttpGet("{id:guid}")]

        public async Task<ActionResult<PostReadDto>> GetPostById(Guid id)
        {
            PostReadDto post = await _postManager.GetPostById(id);
            if(post == null) return NotFound();
            
            return Ok(post);
        }

        #endregion

        #region DeletePost
        //DELETE /api/post/{id}
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
