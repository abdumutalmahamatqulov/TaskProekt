using System.Text.RegularExpressions;

namespace TaskProekt.ExensionFuntions;

public class CheckEmail
{
	public static bool IsValidEmail(string email)
	{
		const string emailRegex = @"^[a-zA-Z0-9]+[\.]?([a-zA-Z0-9]+)?[\@[a-z]{2,9}[\.][a-z]{2,5}$";
		var regex = new Regex(emailRegex, RegexOptions.IgnoreCase);
		return regex.IsMatch(email);
	}
	public static bool HaveCapitalLetter(string password)
	{
		return !string.IsNullOrEmpty(password) && password.Any(char.IsUpper);
	}
}
