using System.Security.Claims;
using codersquare.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentManager _commentManager;

        public CommentsController(ICommentManager commentManager)
        {
            _commentManager = commentManager;
        }
        
        #region CreateComment
        // POST /api/comments/{postId}
        [Authorize]
        [HttpPost("{postId:guid}")]
        public async Task<ActionResult> CreateComment(Guid postId, [FromBody] CommentCreateDto commentDto)
        {
            // Get the current user's ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            
            
            await _commentManager.CreateComment(commentDto, userId, postId);
            return CreatedAtAction(nameof(GetComments), new { postId }, "Comment created successfully.");
        }
        #endregion
        
        #region DeleteComment
        // DELETE /api/comments/{id}
        [Authorize]
        [HttpDelete("{id:guid}")]
        public ActionResult DeleteComment(Guid id)
        {
            _commentManager.DeleteComment(id);
            return Ok("Comment deleted successfully.");
        }
        #endregion

        
        #region GetComments
        // GET /api/comments/{postId}
        [HttpGet("{postId:guid}")]
        public async Task<ActionResult<List<CommentReadDto>>> GetComments(Guid postId)
        {
            List<CommentReadDto> comments = await _commentManager.GetComments(postId);
            if (comments == null || comments.Count == 0)
                return NotFound("No comments found for this post.");

            return Ok(comments);
        }
        #endregion

        #region CountComments
        // GET /api/comments/{postId}/count
        [HttpGet("{postId:guid}/count")]
        public async Task<ActionResult<int>> CountComments(Guid postId)
        {
            // Get the comment count for the specific post
            int commentCount = await _commentManager.CountComment(postId);
            return Ok(new { count = commentCount });
        }
        #endregion
        
    }
}
