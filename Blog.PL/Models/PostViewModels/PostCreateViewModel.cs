using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Blog.PL.Models.PostViewModels
{
    public class PostCreateViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string? imageURL { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public bool Pinned { get; set; }
        public List<SelectListItem>?Tags{ get; set; }
        public int[] SelectedTags { get; set; }
        
    }
}
