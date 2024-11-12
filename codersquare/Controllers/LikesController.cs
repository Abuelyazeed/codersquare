using codersquare.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeManager _likeManager;

        public LikesController(ILikeManager likeManager)
        {
            _likeManager = likeManager;
        }

        #region LikePost
        // POST /api/posts/{postId}/likes
        [HttpPost]
        public async Task<ActionResult> LikePost([FromBody] LikeCreateDto likeCreateDto)
        {
            await _likeManager.CreateLike(likeCreateDto);
            return Ok("Like added successfully.");
        }
        #endregion

        
    }
}
