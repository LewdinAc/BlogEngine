using BlogEngineApi.DTOs.Post;
using BlogEngineApi.Services;
using BlogEngineDb;
using BlogEngineDb.Enums;
using BlogEngineDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineTests.ServiceTests
{
    public class PostServiceTest
    {
        [Fact]
        public async void CreatePostReturnsOk()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = new PostInfDto()
            {
                Title = "Test",
                Content = "Test"
            };

            //act
            int result = await postsService.CreateAsync("2", post);

            //assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async void CreatePost_EmptyTitle_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            PostInfDto post = new PostInfDto()
            {
                Content = "Test"
            };

            //act
            int result = await postsService.CreateAsync("jose", post);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void CreatePost_EmptyContent_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            PostInfDto post = new PostInfDto()
            {
                Title = "Test"
            };

            //act
            int result = await postsService.CreateAsync("jose", post);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void CreatePost_NoAuthor_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            PostInfDto post = new PostInfDto()
            {
                Title = "Test",
                Content = "Test"
            };

            //act
            int result = await postsService.CreateAsync(null, post);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void UpdatePost_AllFieldsOk_Returns1()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = databaseContext.Posts.First();
            var postInfo = new PostInfDto() { Title = "Test", Content = "Test" };

            //act
            int result = await postsService.UpdateAsync(post.Id, postInfo);

            //assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async void UpdatePost_IdNotExists_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = databaseContext.Posts.First();
            var postInfo = new PostInfDto() { Title = "Test", Content = "Test" };

            //act
            int result = await postsService.UpdateAsync(5, postInfo);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void UpdatePost_EmptyTitle_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = databaseContext.Posts.First();
            var postInfo = new PostInfDto() { Title = string.Empty, Content = "Test" };

            //act
            int result = await postsService.UpdateAsync(post.Id, postInfo);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void UpdatePost_EmptyContent_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = databaseContext.Posts.First();
            var postInfo = new PostInfDto() { Title = "Test", Content = string.Empty };

            //act
            int result = await postsService.UpdateAsync(post.Id, postInfo);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void UpdatePostStatus_FromNewToPendingApproval_Returns1()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = databaseContext.Posts.First();

            //act
            int result = await postsService.UpdateStatusAsync(post.Id, PostStatusEnum.PendingApproval);

            //assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async void UpdatePostStatus_FromNewToApprove_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = databaseContext.Posts.First();

            //act
            int result = await postsService.UpdateStatusAsync(post.Id, PostStatusEnum.Approved);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void UpdatePostStatus_FromNewToRejected_Returns0()
        {
            //arrange
            var databaseContext = SeedDatabaseAddPostForUpdate();
            var postsService = new PostsService(databaseContext);
            var post = databaseContext.Posts.First();

            //act
            int result = await postsService.UpdateStatusAsync(post.Id, PostStatusEnum.Rejected);

            //assert
            Assert.Equal(0, result);
        }

        private DatabaseContext SeedDatabaseAddPostForUpdate()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                            .UseInMemoryDatabase("BlogEngine")
                            .Options;

            var databaseContext = new DatabaseContext(options);
            databaseContext.Database.EnsureCreated();

            var posts = databaseContext.Posts.ToList();
            databaseContext.Posts.RemoveRange(posts);

            Post post = new Post
            {
                Id = 1,
                Author = "Test",
                Created = DateTime.UtcNow,
                Title = "Test",
                Content = "Test",
                Status = PostStatusEnum.New
            };

            databaseContext.Posts.Add(post);

            databaseContext.SaveChanges();

            return databaseContext;
        }
    }
}