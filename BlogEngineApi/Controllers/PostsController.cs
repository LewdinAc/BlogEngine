using BlogEngineApi.DTOs.Post;
using BlogEngineApi.Services;
using BlogEngineDb.Enums;
using BlogEngineDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngineApi.Controllers
{
    [Authorize]
    [Route("api/post")]
    [ApiController]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly PostsService _postsService;
        private readonly CommentsService _commentsService;

        public PostsController(PostsService postsService, CommentsService commentsService)
        {
            _postsService = postsService;
            _commentsService = commentsService;
        }

        /// <summary>
        /// Get the list of published posts
        /// </summary>
        /// <param name="postStatus">Post status</param>
        /// <returns></returns>
        [Authorize(Roles = "reader")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<Post>>> GetPublished()
        {
            //var s = User.GetSubjectId()
            var userId = User.FindFirstValue("userId");

            var posts = await _postsService.GetAsync(userId);

            if (!posts.Any())
                return NoContent();

            return Ok(posts);
        }

        /// <summary>
        /// Get a post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "reader")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var post = await _postsService.GetAsync(id);

            if (post == null)
                return NotFound();

            return post;
        }

        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="postInformation">Post information</param>
        [Authorize(Roles = "writer")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] PostInfDto postInformation)
        {
            var userId = User.FindFirstValue("userId");

            await _postsService.CreateAsync(userId, postInformation);

            return Ok();
        }

        /// <summary>
        /// Update post information
        /// </summary>
        /// <param name="postInformation">Post information</param>
        [Authorize(Roles = "writer")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] PostInfDto postInformation)
        {
            await _postsService.UpdateAsync(id, postInformation);

            return Ok();
        }

        /// <summary>
        /// Submit post for approval
        /// </summary>
        /// <param name="id">Post id</param>
        [Authorize(Roles = "writer")]
        [HttpPut("{id}/submit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateAsPublished([FromRoute] int id)
        {
            await _postsService.UpdateStatusAsync(id, PostStatusEnum.PendingApproval);

            return Ok();
        }

        /// <summary>
        /// Approve post to published
        /// </summary>
        /// <param name="id">Post id</param>
        [Authorize(Roles = "editor")]
        [HttpPut("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateAsApproved([FromRoute] int id)
        {
            await _postsService.UpdateStatusAsync(id, PostStatusEnum.Approved);

            return Ok();
        }

        /// <summary>
        /// Reject post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <param name="id">Comment to add to the post (optional)</param>
        [Authorize(Roles = "editor")]
        [HttpPut("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateAsRejected([FromRoute] int id, string comment)
        {
            await _postsService.UpdateStatusAsync(id, PostStatusEnum.Rejected);

            if (!comment.IsNullOrEmpty())
            {
                await _commentsService.CreateAsync(id, comment, isRejection: true);
            }

            return Ok();
        }
    }
}