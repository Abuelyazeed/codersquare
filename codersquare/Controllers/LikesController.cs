using codersquare.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api/likes")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeManager _likeManager;

        public LikesController(ILikeManager likeManager)
        {
            _likeManager = likeManager;
        }

        #region LikePost
        //TODO get post id from url
        // POST /api//likes/{postId}
        [HttpPost]
        public async Task<ActionResult> LikePost([FromBody] LikeCreateDto likeCreateDto)
        {
            await _likeManager.CreateLike(likeCreateDto);
            return Ok("Like added successfully.");
        }
        #endregion
        
        //Todo
        //List Likes GET /api/likes/{postId}
        //Delete Likes DELETE /api/likes/{postId}

        
    }
}
