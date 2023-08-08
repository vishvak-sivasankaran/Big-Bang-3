﻿using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;
        private readonly string _jwtSecret = "JVr87UcOj69Kqw5R2Nmf4FWs03HdxSjhuyg57UVHnsdufhiw9687Hh";

        public UserController(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("getUsersByRole")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole()
        {
            var users = await _userRepository.GetAllUsersByRole();
            return Ok(users);
        }

        [HttpGet("getAgentByRole")]
        public async Task<ActionResult<IEnumerable<User>>> GetAgentByRole()
        {
            var users = await _userRepository.GetAllAgentByRole();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(User user)
        {
            if (_userRepository == null)
            {
                return Problem("User repository is null.");
            }

            // Encrypt the password before storing it
            user.Password = Encrypt(user.Password);

            var createdUser = await _userRepository.AddUser(user);

            // Generate JWT token with user details
            var token = GenerateJwtToken(createdUser);

            // Return the token as part of the response
            return Ok(token);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] User loginModel)
        {
            if (_userRepository == null)
            {
                return Problem("User repository is null.");
            }

            // Find the user by their email
            var existingUser = await _userRepository.GetUserByEmail(loginModel.UserEmail);

            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Decrypt the stored password and compare it with the provided password
            var decryptedPassword = Decrypt(existingUser.Password);
            if (loginModel.Password != decryptedPassword)
            {
                return Unauthorized("Invalid credentials");
            }

            // Passwords match, generate JWT token with user details
            var token = GenerateJwtToken(existingUser);

            // Return the token as part of the response
            return Ok(token);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.UserEmail),
             }),
                Expires = DateTime.UtcNow.AddDays(1), 
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string Encrypt(string password)
        {
            // Example key and IV generation using hashing
            string passphrase = "your-passphrase";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
                byte[] iv = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase)).Take(16).ToArray();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(password);
                            }
                        }

                        byte[] encryptedData = memoryStream.ToArray();
                        return Convert.ToBase64String(encryptedData);
                    }
                }
            }
        }
        private string Decrypt(string encryptedPassword)
        {
            // Example key and IV generation using hashing
            string passphrase = "your-passphrase";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
                byte[] iv = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase)).Take(16).ToArray();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    byte[] encryptedData = Convert.FromBase64String(encryptedPassword);

                    using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cryptoStream))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

    }
}




       
        

       


