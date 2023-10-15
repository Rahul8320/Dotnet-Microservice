using Mango.Services.AuthAPI.DB;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dtos;
using Mango.Services.AuthAPI.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(
            AppDbContext context, 
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager
        )
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            // retrieve user from db
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Email != null && u.Email.Equals(email));

            if (user != null)
            {
                // check if the role is exists or not
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // create role if it does not exist.
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                // assign the role to user
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            
            // user is null.
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName != null && u.UserName.Equals(loginRequestDto.UserName));

                bool isValid = user != null && await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(user == null || isValid == false)
                {
                    return new LoginResponseDto() { User=null, Token=""};
                }

                // if user was found, generate the jwt token
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateJwtToken(user, roles);

                UserDto userDto = new() { 
                    Email = user.Email ?? "",
                    ID = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber ?? ""
                };

                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    User = userDto,
                    Token = token
                };

                return loginResponseDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                ApplicationUser user = new()
                {
                    UserName = registrationRequestDto.Email,
                    Email = registrationRequestDto.Email,
                    NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                    Name = registrationRequestDto.Name,
                    PhoneNumber = registrationRequestDto.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    var userDetails = _context.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    if(userDetails != null)
                    {
                        return "";
                    }
                }
                else
                {
                    return result.Errors.First().Description;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return "Register Failed";
        }
    }
}
