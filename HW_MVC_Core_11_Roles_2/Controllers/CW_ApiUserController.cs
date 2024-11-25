using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using HW_MVC_Core_11_Roles_2.Models;
using LoginModel = HW_MVC_Core_11_Roles_2.Models.LoginModel;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HW_MVC_Core_11_Roles_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CW_ApiUserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        public CW_ApiUserController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginModel model)
        {
            // Перевіряємо, що email та пароль були передані
            if (!ModelState.IsValid)
            {
                return BadRequest("Is not valid model");
            }
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            // Используем UserManager для создания пользователя с переданным паролем
            var result = await _userManager.CreateAsync(user, model.Password);

            // Если создание прошло успешно
            if (result.Succeeded)
            {
                // Возвращаем успешный ответ с сообщением
                return Ok("User register successfully ...");
            }

            // Если email или пароль отсутствуют, возвращаем ошибку
            //return BadRequest("Email и пароль обязательны.");
            return BadRequest(result.Errors);
            
            // Создаем нового пользователя IdentityUser с указанным email

            // Возвращаем ошибки, если не удалось создать пользователя

        }
        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(LoginModel model)
        {
            // Проверяем, что email и пароль были переданы
            if (!ModelState.IsValid)
            {
                return BadRequest("Is not valid model");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) 
            {
                return BadRequest("Invalid email...");
            }

            //var result = await _signInManager.PasswordSignInAsync(
            //    model.Email,
            //    model.Password,
            //    isPersistent: false,
            //    lockoutOnFailure: false
            //    );
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            return BadRequest("Invalid email or password...");
        }
        public async Task<IActionResult> AccessDenied()
        {
            return BadRequest("Cookie: Access Denied ...");
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),

                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]

            };var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
