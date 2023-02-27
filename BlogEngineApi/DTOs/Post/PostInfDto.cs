using System.ComponentModel.DataAnnotations;

namespace BlogEngineApi.DTOs.Post
{
    public class PostInfDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(450)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}