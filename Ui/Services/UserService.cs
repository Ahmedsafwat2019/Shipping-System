using BL.Contract;
using BL.Dtos;
using DAL.UserModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Ui.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = accessor;
        }

        public async Task<UserResultDto> RegisterAsync(UserDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return new UserResultDto { Success = false, Errors = new[] { "Passwords do not match." } };
            }

            var user = new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email,
            FirstName=registerDto.FirstName,LastName=registerDto.LastName,Phone=registerDto.Phone};
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var roleName = (string.IsNullOrEmpty(registerDto.Role)) ? "User" : registerDto.Role;

            var roleResult = await _userManager.AddToRoleAsync(user, roleName);

            if(!roleResult.Succeeded)
            {
                return new UserResultDto
                {
                    Success = false,
                    Errors = roleResult.Errors?.Select(e => e.Description)
                };
            }

            return new UserResultDto
            {
                Success = result.Succeeded,
                Errors = result.Errors?.Select(e => e.Description)
            };
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new UserResultDto
                {
                    Success = false,
                    Errors = new[] { "User with this email does not exist." }
                };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordValid)
            {
                return new UserResultDto
                {
                    Success = false,
                    Errors = new[] { "Incorrect password." }
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

            if (!result.Succeeded)
            {
                return new UserResultDto
                {
                    Success = false,
                    Errors = new[] { "Login failed. Please try again." }
                };
            }

            return new UserResultDto
            {
                Success = true,
                Errors = null,
                Token = null
                // Any other user info you want to return
            };
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Phone=user.Phone,
                Role= roles.FirstOrDefault()
            };
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = roles.FirstOrDefault() // ✅ مهم جدا هنا تكون راجعة القيمة الصح
            };
        }


        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users;
            return users.Select(u => new UserDto
            {
                Id = Guid.Parse(u.Id),
                Email = u.Email,
            });
        }

        public Guid GetLoggedInUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId);
        }
    }

}
