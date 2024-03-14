using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolSystem.DTOs.Identity;
using SchoolSystem.School.DAL.Data.Identity;
using SchoolSystem.School.DAL.Data.Models.Admin;
using SchoolSystem.School.DAL.Data.Models.AuthJWT;
using SchoolSystem.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolSystem.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthoServices _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthoServices authService, IConfiguration configuration  /*, UserManager<IdentityUser> userManager*/)
        {
            _authService = authService;
            _configuration = configuration;
            //_userManager = userManager;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterAsync([FromBody] Registration model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _authService.RegisterAsync(model);

        //    if (!result.IsAuthenticated)
        //        return BadRequest(result.Message);

        //    return Ok(result);
        //}

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenReqmodel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        //[HttpPost("addrole")]
        //public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _authService.AddRoleAsync(model);

        //    if (!string.IsNullOrEmpty(result))
        //        return BadRequest(result);

        //    return Ok(model);
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.UserName);
        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        // Assuming your authService now internally manages how to generate a JWT token
        //        var tokenResult = await _authService.GenerateJwtToken(user);

        //        if (tokenResult.IsAuthenticated)
        //        {
        //            return Ok(new
        //            {
        //                token = tokenResult.Token,
        //                expiration = tokenResult.Expiration
        //            });
        //        }
        //    }
        //    return Unauthorized();
        //}

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto user)
        {
           
            if (user.UserName == "admin" && user.Password == "password")
            {
                var token = GenerateJwtToken();
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public async Task<IActionResult> test()
        //{
        //    var master = new AppUser()
        //    {
        //        FirstName = "mo",
        //        Email="mo@Example.cpm",
        //        UserName = "linkits",
                

        //        //AppUser app = new AppUser (){ }
        //        //Username = "linkits",
        //        //Password = "Link@12345"
        //    };
        //    await _userManager.CreateAsync(master , password: "Link@12345");
        //    return Ok();
             
        //}



    }
}

