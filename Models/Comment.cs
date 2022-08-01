using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        [StringLength(250)]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }
        
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}