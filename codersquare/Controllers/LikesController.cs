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
        //TODO get user id
        // POST /api/likes/{postId}
        [HttpPost("{postId:guid}")]
        public async Task<ActionResult> LikePost(Guid postId,[FromBody] LikeCreateDto likeCreateDto)
        {
            await _likeManager.CreateLike(postId, likeCreateDto);
            return Ok("Like added successfully.");
        }
        #endregion

        #region CountLikes
        //GET /api/likes/{oostId}
        [HttpGet("{postId:guid}")]
        public async Task<ActionResult<int>> GetLikesCount(Guid postId)
        {
            int likeCount = await _likeManager.GetLikesCount(postId);
            return Ok(likeCount);
        }
        #endregion

        #region DeleteLike
        //Todo get userId
        //DELETE api/likes/{postId}/{userId}
        [HttpDelete("{postId:guid}/{userId:guid}")]
        public async Task<ActionResult<int>> DeleteLike(Guid postId, Guid userId)
        {
            bool success = await _likeManager.DeleteLike(postId, userId);
            if (success)
            {
                return Ok("Like deleted successfully.");
            }
            return NotFound("Like not found.");
        }

        #endregion
    }
}
