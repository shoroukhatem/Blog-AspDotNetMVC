using System.ComponentModel.DataAnnotations;

namespace Blog.PL.Models
{
	public class RegisterViewModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		[MinLength(6)]
		public string Password { get; set; }
		[Required]
		[MinLength(6)]
		[Compare(nameof(Password), ErrorMessage = "Password Mismatch")]
		public string ConfirmPassword { get; set; }
	}
}
