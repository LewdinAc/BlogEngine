using BlogEngineApi.Infrastructure;
using BlogEngineDb;
using BlogEngineDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogEngineApi.Services
{
    public class CommentsService
    {
        private readonly DatabaseContext _databaseContext;

        public CommentsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Comment>> GetAsync(string userId, int postId)
        {
            var user = TestUsers.GetUserById(userId);
            var rolValues = user.Claims.Where(c => c.Type == "rols").Select(c => c.Value).ToList();
            var isReader = !rolValues.Any(c => c != "reader");

            if (isReader)
                return await _databaseContext.Comments.Where(c => c.PostId == postId && !c.IsRejection).ToListAsync();

            return await _databaseContext.Comments.Where(c => c.PostId == postId).ToListAsync();
        }

        public async Task<int> CreateAsync(int postId, string comment, bool isRejection = false)
        {
            if(comment.IsNullOrEmpty())
                return 0;

            _databaseContext.Comments.Add(new Comment()
            {
                PostId = postId,
                Content = comment,
                IsRejection = isRejection
            });

            return await _databaseContext.SaveChangesAsync();
        }
    }
}