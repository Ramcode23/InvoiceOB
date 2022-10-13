using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Domain.Indentity;
using Microsoft.AspNetCore.Identity;

using Persistence;
using Services.DTOs.User;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
      
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<ApplicationUser> signInManager,
                            ApplicationDbContext context,
                            IConfiguration configuration
            )
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;

        }
        public async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
           await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
         var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<IdentityResult> CreateAdminAsync(UserRegister registeruser)
        {
            var user = new ApplicationUser
            {
                UserName = registeruser.Email,
                Email = registeruser.Email,
                FirstName = registeruser.FirstName,
                LastName = registeruser.LastName,
            };

            var rest = await _userManager.CreateAsync(user, registeruser.Password);
            if (rest.Succeeded)
                await _userManager.AddClaimAsync(user, new Claim("role", "admin"));

            return rest;
        }

     

        public async Task<IList<Claim>> GetRoleAsync(ApplicationUser user)
        {
           return await _userManager.GetClaimsAsync(user);
        }

        public async Task<SignInResult> PasswordSignInAsync(UserLogin credentials)
        {
          return await _signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegister registeruser)
        {
             var user = new ApplicationUser
            {
                UserName = registeruser.Email,
                Email = registeruser.Email,
                FirstName = registeruser.FirstName,
                LastName = registeruser.LastName,
            };

            var rest = await _userManager.CreateAsync(user, registeruser.Password);
            if (rest.Succeeded)
                await _userManager.AddClaimAsync(user, new Claim("role", "user"));

            return rest;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user;
        }
    }
}