using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopBridge.Product.Business;
using ShopBridge.Product.DataBase.Identity;
using ShopBridge.Product.Model.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        public readonly UserManager<ShopBridgeUser> _ShopBridgeUser;
        public readonly RoleManager<IdentityRole> _ShopBridgeRole;
        public readonly IConfiguration _configuration;
        public IdentityController(UserManager<ShopBridgeUser> ShopBridgeUser, RoleManager<IdentityRole> ShopBridgeRole, IConfiguration configuration)
        {
            _ShopBridgeUser = ShopBridgeUser;
            _ShopBridgeRole = ShopBridgeRole;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser userdata)
        {
            var data = await _ShopBridgeUser.FindByNameAsync(userdata.UserName);
            if (data != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "", message = "already exist" });

            ShopBridgeUser user = new ShopBridgeUser()
            {
                Email = userdata.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userdata.UserName
            };
            var respose = await _ShopBridgeUser.CreateAsync(user, userdata.Password);
            if (!respose.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "", message = "failed" });
            }
            if (!await _ShopBridgeRole.RoleExistsAsync("admin"))
                await _ShopBridgeRole.CreateAsync(new IdentityRole("admin"));
            if (!await _ShopBridgeRole.RoleExistsAsync("user"))
                await _ShopBridgeRole.CreateAsync(new IdentityRole("user"));
            if (await _ShopBridgeRole.RoleExistsAsync("admin"))
            {
                await _ShopBridgeUser.AddToRoleAsync(user, "admin");
            }

            return Ok(new { status = "200", message = "Success created" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User userdata)
        {
            var data = await _ShopBridgeUser.FindByNameAsync(userdata.UserName);
            if (data != null && await _ShopBridgeUser.CheckPasswordAsync(data, userdata.Password))
            {
                var roles = await _ShopBridgeUser.GetRolesAsync(data);
                var authclaims = new List<Claim> {
                new Claim(ClaimTypes.Name,userdata.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach (var item in roles)
                {
                    authclaims.Add(new Claim(ClaimTypes.Role, item));
                }

                var authassignkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTData:SIGN_KEY"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWTData:ValidIssuer"],
                    audience: _configuration["JWTData:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authclaims,
                    signingCredentials: new SigningCredentials(authassignkey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }
    }
}
