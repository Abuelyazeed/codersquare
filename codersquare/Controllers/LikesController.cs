using System.Security.Claims;
using codersquare.BL;
using Microsoft.AspNetCore.Authorization;
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
        // POST /api/likes/{postId}
        [Authorize]
        [HttpPost("{postId:guid}")]
        public async Task<ActionResult> LikePost(Guid postId)
        {
            // Get the current user's ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            
            await _likeManager.CreateLike(postId, userId);
            return Ok("Like added successfully.");
        }
        #endregion
        
        #region DeleteLike
        //DELETE api/likes/{postId}/{userId}
        [Authorize]
        [HttpDelete("{postId:guid}")]
        public async Task<ActionResult<int>> DeleteLike(Guid postId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            
            bool success = await _likeManager.DeleteLike(postId, userId);
            if (success)
            {
                return Ok("Like deleted successfully.");
            }
            return NotFound("Like not found.");
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
        
    }
}
