using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Indentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence;
using Services.DTOs.User;

namespace Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ClaimsIdentity _claimsIdentity;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<ApplicationUser> signInManager,
                            ClaimsIdentity claimsIdentity,
                            ApplicationDbContext context,
                            IConfiguration configuration
            )
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _claimsIdentity = claimsIdentity;
            _context = context;
            _configuration = configuration;

        }
        public Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CheckRoleAsync(string roleName)
        {
            throw new NotImplementedException();
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

        public async Task<ApplicationUser> GetAuthenticatedUserAsync(ClaimsPrincipal User)
        {
            var authenticatedUser = User.Identities.Select(c => c.Claims).ToArray()[0].ToArray()[0].Value;

            return await _userManager.FindByEmailAsync(authenticatedUser);
        }

        public string GetAuthenticaedUserName(ClaimsPrincipal User)
        {
             return User.Identities.Select(c => c.Claims).ToArray()[0].ToArray()[0].Value;
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
    }
}