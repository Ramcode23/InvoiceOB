using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Indentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services;
using Services.DTOs.User;
using UniversityApiBackend.Helpers;

namespace envoiceBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IStringLocalizer<AccountController> _stringLocalizer;
        private readonly IUserService _userService;
        public AccountController(IUserService userService,
                                 JwtSettings jwtSettings
                                 
        )
        {
            _jwtSettings = jwtSettings;
            _userService = userService;
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserLogin>> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var Token = new UserTokens();
                //var valid = Logins.Any(user => user.Name.Equals(userLogin.Password, StringComparison.OrdinalIgnoreCase));
                var valid = await _userService.PasswordSignInAsync(userLogin);


                if (valid.Succeeded)
                {
                    var user = await _userService.GetUserByEmailAsync(userLogin.UserName);
                    var rol = await _userService.GetRoleAsync(user);

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = userLogin.UserName,
                        EmailId = userLogin.UserName,
                        Rol = rol[0].Value,
                        GuidId = Guid.NewGuid(),

                    },
                    _jwtSettings);
                }
                else
                {
                  
                    return BadRequest("Wrong password");
                }

                var msj = "Welcome";


                return Ok(new
                {

                    Token = Token,
                    Msj = msj
                });
            }
            catch (Exception ex)
            {

                throw new Exception("GetToken Error", ex);
            }

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserLogin>> Register([FromBody] UserRegister registeruser)
        {
            var Token = new UserTokens();
            var isExits = await _userService.GetUserByEmailAsync(registeruser.Email);
            if (isExits == null)
            {
                var rest = await _userService.RegisterUserAsync(registeruser);
                var credencials = new UserLogin { Password = registeruser.Password };
                if (rest.Succeeded)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = registeruser.Email,
                        EmailId = registeruser.Email,
                        GuidId = Guid.NewGuid(),
                        Rol = "user"
                    },
                 _jwtSettings);


                    return Ok(new
                    {

                        Token = Token,
                        Msj = "Welcome"
                    });
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }

        [HttpPost("createmanager")]
     // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<UserLogin>> CreateAdmin([FromBody] UserRegister registeruser)
        {
            var Token = new UserTokens();
            var isExits = await _userService.GetUserByEmailAsync(registeruser.Email);

            if (isExits == null)
            {
                var rest = await _userService.CreateAdminAsync(registeruser);
                var credencials = new UserLogin { Password = registeruser.Password };
                if (rest.Succeeded)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = registeruser.Email,
                        EmailId = registeruser.Email,
                        GuidId = Guid.NewGuid(),
                        Rol = "Admin"

                    },
                 _jwtSettings);


                   

                    return Ok(new
                    {

                        Token = Token,
                        Msj = "Welcome"
                    });
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUseProlife()
        {

            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            if(user==null)
            return BadRequest("User does not exist");

            return Ok( new
            {
                UserName = user.UserName,
                Email = user.Email,
                FistName= user.FirstName,
                LastName = user.LastName,

            });
        }
    }



}
