using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Blog.DAL.Entities
{
    public class Post:BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string? imageURL { get; set; }
        [Required]
        public bool Pinned { get; set; }
        [Required]
        public string UserId {  get; set; }
        public List<PostTag> PostTags { get; set; }=new List<PostTag>();

    }
}
