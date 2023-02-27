using BlogEngineDb.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineDb.Models
{
    public class Post
    {
        public Post()
        {
            Created = DateTime.UtcNow;
            Status = PostStatusEnum.New;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? PublishDate { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public PostStatusEnum Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}