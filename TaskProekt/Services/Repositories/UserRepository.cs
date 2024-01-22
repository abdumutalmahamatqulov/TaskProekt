using Microsoft.AspNetCore.Identity;
using TaskProekt.Data;
using TaskProekt.Dto_s;
using TaskProekt.Entities;
using TaskProekt.Entities.Enum;
using TaskProekt.ExensionFuntions;
using TaskProekt.Services.Interfaces;
using TaskProekt.ViewModels;

namespace TaskProekt.Services.Repositories;

public class UserRepository : IUserRepository
{
	readonly AppDbContext _context;
	readonly UserManager<User> _userManager;
	readonly SignInManager<User> _signInManager;

	public UserRepository(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_context = context;
		_userManager = userManager;
		_signInManager = signInManager;
	}
	public async  Task<RegistorDto> Register(RegistorDto model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
		throw new Exception("Invalid email address format");
		var exitUser = await _userManager.FindByEmailAsync(model.Email);
		if (exitUser != null)
			throw new Exception("Email already token");
		var user = new User { UserName = model.Name, Email = model.Email };
		var result = await _userManager.CreateAsync(user, model.Password);
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "User");
			await _context.SaveChangesAsync();
		}
		foreach (var error in result.Errors)
			throw new Exception($"{error.Description}");
		return model ?? new RegistorDto();
	}

	public async  Task<CreateUserModel> CreateUser(CreateUserModel model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
			throw new Exception("Invalid email address format");
		var exitUser = await _userManager.FindByEmailAsync(model.Email);
		if (exitUser != null)
			throw new Exception("Email already token");
		var user = new User
		{
			UserName = model.Name,
			Email = model.Email,
		};
		var result = await _userManager.CreateAsync(user, model.Password);
		Console.WriteLine(result.Errors);
		if (model.Role == ERole.MANAGER)
		{
			if (!result.Succeeded) return model ?? new CreateUserModel();
			await _userManager.AddToRoleAsync(user, "MANAGER");
			await _context.SaveChangesAsync();
		}
		else if (model.Role == ERole.USER)
		{
			if (!result.Succeeded) return model ?? new CreateUserModel();
			await _userManager.AddToRoleAsync(user, "USER");
			await _context.SaveChangesAsync();
		}
		else if (model.Role == ERole.ADMIN)
		{
			if (!result.Succeeded) return model ?? new CreateUserModel();
			await _userManager.AddToRoleAsync(user, "ADMIN");
			await _context.SaveChangesAsync();
		}
		return model ?? new CreateUserModel();
	}

	public async  Task<SignInResult> Login(LoginDto model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
			throw new Exception("Invalid email address format");
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null) throw new Exception("Invalid email or password");
		var passResult = await _userManager.CheckPasswordAsync(user, model.Password);

		if (!passResult)
			throw new Exception("Invalid email or password");
		var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
		if (!result.Succeeded)
			throw new Exception("Invalid email or password");

		return result;
	}


	public async  Task<RegistorDto> RegisterManager(RegistorDto model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
			throw new Exception("Invalid email address format");
		var exitUser = await _userManager.FindByEmailAsync(model.Email);
		if (exitUser != null)
			throw new Exception("Email already token");
		var user = new User
		{
			UserName = model.Name,
			Email = model.Email,
		};
		var result = await _userManager.CreateAsync(user, model.Password);
		Console.WriteLine(result.Errors);
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "MANAGER");
			await _context.SaveChangesAsync();
		}
		foreach (var error in result.Errors)
			throw new Exception($"{error.Description}");
		return model ?? new RegistorDto();
	}
}
