using System.ComponentModel.DataAnnotations;

namespace Blog.PL.Models
{
    public class PostTagViewModel
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string? imageURL { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public bool Pinned { get; set; }
        public string UserId { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherPost = (PostTagViewModel)obj;
            // Compare properties that determine equality
            return Id == otherPost.Id;
        }

        // Override GetHashCode method for proper hashing
        public override int GetHashCode()
        {
            // Hash properties that determine equality
            return HashCode.Combine(Id);
        }
    }
}
