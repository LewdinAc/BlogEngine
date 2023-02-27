using BlogEngineDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngineDb.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.Author, x.Status }, "IX_Post");

            builder.HasMany(x => x.Comments).WithOne(x => x.Post);
        }
    }
}