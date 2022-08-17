using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService tokenService;

        public AccountController(DataContext context,ITokenService tokenService)
        {
            _context = context;
            this.tokenService = tokenService;
        }
        // the data that is sent to post request whether it came from query string or inside the body [ApiController] handles it for us
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
            //BadRequest() is a built in method that came from ACtionRest 
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA256();
            var user = new AppUser{
               UserName = registerDto.Username.ToLower(),
               // below we are parsing the sent password which is string into hash
               passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
               //we again encrypt it what we call password Salt
               passwordSalt = hmac.Key,
              
            }; 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto{
                UserName = user.UserName,
                Token  = this.tokenService.createToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if(user == null) return Unauthorized("Invalid Username");
            // we decrypt the password salt that is on our db to hash
            using var hmac = new HMACSHA256(user.passwordSalt);
            // we are encrypting to hash the password that received from the user
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i=0; i<ComputeHash.Length; i++){

                if(ComputeHash[i] != user.passwordHash[i]) return Unauthorized("Invalid password");
            
            }
             return new UserDto{
                UserName = user.UserName,
                Token  = this.tokenService.createToken(user),
            };

        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
  
    }
}