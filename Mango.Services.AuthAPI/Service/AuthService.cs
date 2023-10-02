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

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(user == null || isValid == false)
                {
                    return new LoginResponseDto() { User=null, Token=""};
                }

                // if user was found, generate the jwt token

                var token = _jwtTokenGenerator.GenerateJwtToken(user);

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
