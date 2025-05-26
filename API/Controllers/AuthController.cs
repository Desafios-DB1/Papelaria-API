using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Crosscutting.Dtos.Auth.Login;
using Crosscutting.Dtos.Auth.Register;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Senha))
                return Unauthorized("Usuário ou senha inválidos.");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroRequestDto request)
        {
            var user = new Usuario { UserName = request.NomeUsuario, NomeUsuario = request.NomeUsuario, Email = request.Email, NomeCompleto = request.NomeCompleto, DataCriacao = DateTime.Now};

            var result = await _userManager.CreateAsync(user, request.Senha);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Usuário registrado com sucesso!");
        }

        private async Task<string> GenerateJwtToken(Usuario user)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new(ClaimTypes.Role, role));
            }

            var jwtSecret = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
                throw new InvalidOperationException("JWT Secret is not configured.");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
