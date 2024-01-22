using System.ComponentModel.DataAnnotations;

namespace TaskProekt.Dto_s;

public class RegistorDto
{
	public string Name { get; set; }
	[EmailAddress(ErrorMessage = "Invalid Email Address")]
	public string Email { get; set; }
	public string Password { get; set; }
	public string ConfirmPassword { get; set; }
}
