using System.ComponentModel.DataAnnotations;

namespace Blog.PL.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
		[MinLength(6)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
