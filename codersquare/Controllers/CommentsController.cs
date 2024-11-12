using codersquare.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codersquare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentManager _commentManager;

        public CommentsController(ICommentManager commentManager)
        {
            _commentManager = commentManager;
        }
        
        #region CreateComment
        // POST /api/comments/{userId}/{postId}
        [HttpPost("{userId:guid}/{postId:guid}")]
        public async Task<ActionResult> CreateComment(Guid userId, Guid postId, [FromBody] CommentCreateDto commentDto)
        {
            await _commentManager.CreateComment(commentDto, userId, postId);
            return CreatedAtAction(nameof(GetComments), new { postId }, "Comment created successfully.");
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
        
        #region DeleteComment
        // DELETE /api/comments/{id}
        [HttpDelete("{id:guid}")]
        public ActionResult DeleteComment(Guid id)
        {
            _commentManager.DeleteComment(id);
            return Ok("Comment deleted successfully.");
        }
        #endregion
    }
}
