using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Indentity;
using Microsoft.AspNetCore.Identity;
using Services.DTOs.User;

namespace Services
{
    public interface IUserService
    {
         Task<ApplicationUser> GetAuthenticatedUserAsync(ClaimsPrincipal User);
        string GetAuthenticaedUserName(ClaimsPrincipal User);
        Task<SignInResult> PasswordSignInAsync(UserLogin credentials);
        Task<IdentityResult> RegisterUserAsync(UserRegister registeruser);
        Task<IdentityResult> CreateAdminAsync(UserRegister registeruser);
        Task AddUserToRoleAsync(ApplicationUser user, string roleName);
        Task CheckRoleAsync(string roleName);
        Task<IList<Claim>> GetRoleAsync(ApplicationUser user);
    }
}