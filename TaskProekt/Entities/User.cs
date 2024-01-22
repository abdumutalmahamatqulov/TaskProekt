using Microsoft.AspNetCore.Identity;

namespace TaskProekt.Entities;

public class User:IdentityUser
{
	public List<Task> Tasks { get; set; }
}
