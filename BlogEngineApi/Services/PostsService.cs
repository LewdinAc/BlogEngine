using BlogEngineApi.DTOs.Post;
using BlogEngineApi.Infrastructure;
using BlogEngineDb;
using BlogEngineDb.Enums;
using BlogEngineDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogEngineApi.Services
{
    public class PostsService
    {
        private readonly DatabaseContext _databaseContext;

        public PostsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Post>> GetAsync(string userId)
        {
            var posts = new List<Post>();
            var user = TestUsers.GetUserById(userId);

            var rolValues = user.Claims.Where(c => c.Type == "rols").Select(c => c.Value).ToList();
            var isReader = !rolValues.Any(c => c != "reader");

            if (isReader)
                return await _databaseContext.Posts.Where(p => p.Status == PostStatusEnum.Approved).ToListAsync();

            return await _databaseContext.Posts.ToListAsync();
        }

        public async Task<Post?> GetAsync(int id) =>
            await _databaseContext.Posts.FindAsync(id);

        public async Task<int> CreateAsync(string userId, PostInfDto newPost)
        {
            int result = 0;
            if (userId.IsNullOrEmpty() || newPost.Title.IsNullOrEmpty() || newPost.Content.IsNullOrEmpty())
                return result;

            var user = TestUsers.GetUserById(userId);
            Post post = new()
            {
                Title = newPost.Title,
                Content = newPost.Content,
                Author = user.Username,
            };

            _databaseContext.Posts.Add(post);
            result = await _databaseContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(int id, PostInfDto updatedPost)
        {
            int result = 0;

            if (updatedPost.Title.IsNullOrEmpty() || updatedPost.Content.IsNullOrEmpty())
                return result;

            var post = await _databaseContext.Posts.FindAsync(id);
            if (post == null) return result;

            if (post.Status == PostStatusEnum.PendingApproval || post.Status == PostStatusEnum.Approved)
                return result;

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;

            if (post.Status == PostStatusEnum.Approved) 
                post.PublishDate = DateTime.UtcNow;

            _databaseContext.Posts.Update(post);
            result = await _databaseContext.SaveChangesAsync();

            return result;
        }


        public async Task<int> UpdateStatusAsync(int id, PostStatusEnum postStatus)
        {
            int result = 0;
            var post = await _databaseContext.Posts.FindAsync(id);
            if (post == null) return result;

            if (post.Status == PostStatusEnum.PendingApproval &&
                (postStatus != PostStatusEnum.Approved || postStatus != PostStatusEnum.Rejected))
                return result;

            if (post.Status == PostStatusEnum.New && postStatus != PostStatusEnum.PendingApproval)
                return result;

            if (post.Status == PostStatusEnum.Approved)
                return result;

            post.Status = postStatus;

            _databaseContext.Posts.Update(post);
            result = await _databaseContext.SaveChangesAsync();

            return result;
        }
    }
}