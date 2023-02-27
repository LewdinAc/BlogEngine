using System.ComponentModel.DataAnnotations;

namespace BlogEngineDb.Models
{
    public class Comment
    {
        public Comment()
        {
            CreatedDate = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public bool IsRejection { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public virtual Post Post { get; set; }
    }
}