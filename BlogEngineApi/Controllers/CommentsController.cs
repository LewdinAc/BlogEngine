using BlogEngineApi.Services;
using BlogEngineDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngineApi.Controllers
{
    [Authorize]
    [Route("api/post")]
    [ApiController]
    [Produces("application/json")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsService _commentsService;

        public CommentsController(CommentsService commentsService) =>
            _commentsService = commentsService;

        /// <summary>
        /// Get list of post comments by id
        /// </summary>
        /// <param name="postId">Post id to get comments</param>
        /// <returns></returns>
        [Authorize(Roles = "reader")]
        [HttpGet("{postId}/comment")]
        public async Task<List<Comment>> Get(int postId)
        {
            var userId = User.FindFirstValue("userId");
            var comments = await _commentsService.GetAsync(userId, postId);
            return comments;
        }

        /// <summary>
        /// Add new comment to a post
        /// </summary>
        /// <param name="postId">Post id to add the comment</param>
        /// <param name="comment">Text comment to add</param>
        /// <returns></returns>
        [Authorize(Roles = "reader")]
        [HttpPost("{postId}/comment")]
        public async Task<IActionResult> Post([FromRoute] int postId, [FromBody] string comment)
        {
            await _commentsService.CreateAsync(postId, comment);
            return Created(string.Empty, null);
        }
    }
}