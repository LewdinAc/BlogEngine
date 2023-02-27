using BlogEngineApi.Services;
using BlogEngineDb;
using BlogEngineDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineTests.ServiceTests
{
    public class CommentServiceTest
    {
        private readonly CommentsService commentsService;

        public CommentServiceTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                            .UseInMemoryDatabase("BlogEngine")
                            .Options;

            DatabaseContext databaseContext = new DatabaseContext(options);

            databaseContext.Posts.Add(new Post()
            {
                Title = "Test",
                Content = "Test",
                Author = "Test"
            });
            databaseContext.SaveChanges();

            commentsService = new CommentsService(databaseContext);
        }

        [Fact]
        public async Task CreateCommentReturnsOk()
        {
            //arrange
            string comment = "Test comment";

            //act
            int result = await commentsService.CreateAsync(1, comment);

            //assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CreateComment_EmptyText_Returns0()
        {
            //arrange
            string comment = string.Empty;

            //act
            int result = await commentsService.CreateAsync(1, comment);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task CreateComment_NullText_Returns0()
        {
            //arrange
            string? comment = null;

            //act
            int result = await commentsService.CreateAsync(1, comment);

            //assert
            Assert.Equal(0, result);
        }
    }
}