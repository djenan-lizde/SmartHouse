using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHouse.Api.Configuration;
using SmartHouse.Api.Database;
using SmartHouse.Api.Encryption;
using SmartHouse.Models.Models;
using SmartHouse.Models.Requests;
using SmartHouse.Models.Responses;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartHouse.Api.Services
{
    public interface IUserService : IData<User>
    {
        UserAuthenticationResult Authenticate(UserLoginModel model);
        User RegisterUser(UserRegistration user);
    }

    public class UserService : Data<User>, IUserService
    {
        private readonly IOptions<AppSettings> _options;
        public UserService(ApplicationDbContext context, IOptions<AppSettings> options) : base(context)
        {
            _options = options;
        }

        public UserAuthenticationResult Authenticate(UserLoginModel model)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == model.Username);

            if (user == null)
            {
                throw new ArgumentNullException("Error");
            }

            if (user.PasswordHash != HashGenSalt.GenerateHash(user.PasswordSalt, model.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                }),
                Issuer = _options.Value.Issuer,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserAuthenticationResult
            {
                Id = user.Id,
                Token = tokenHandler.WriteToken(token),
                Username = user.Username
            };
        }
        public User RegisterUser(UserRegistration userRegister)
        {
            var userInDbUserName = _context.Users.FirstOrDefault(x => x.Username == userRegister.Username);
            var userInDbEmail = _context.Users.FirstOrDefault(x => x.Email == userRegister.Email);

            if (userInDbUserName != null)
                throw new Exception("Username already in use!");

            if (userInDbEmail != null)
                throw new Exception("Email already in use!");

            if (userRegister.Password != userRegister.PasswordConfirmation)
            {
                throw new Exception("Passwords do not match!");
            }

            var user = new User
            {
                Email = userRegister.Email,
                Username = userRegister.Username,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                JoinDate = DateTime.UtcNow,
                PhoneNumber = userRegister.PhoneNumber
            };

            user.PasswordSalt = HashGenSalt.GenerateSalt();
            user.PasswordHash = HashGenSalt.GenerateHash(user.PasswordSalt, userRegister.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
