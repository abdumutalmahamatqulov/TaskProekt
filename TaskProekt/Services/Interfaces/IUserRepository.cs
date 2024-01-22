using Microsoft.AspNetCore.Identity;
using TaskProekt.Dto_s;
using TaskProekt.ViewModels;

namespace TaskProekt.Services.Interfaces;

public interface IUserRepository
{
	Task<RegistorDto> Register(RegistorDto model);

	Task<RegistorDto> RegisterManager(RegistorDto model);

	Task<SignInResult> Login(LoginDto model);
	Task<CreateUserModel> CreateUser(CreateUserModel model);
}
