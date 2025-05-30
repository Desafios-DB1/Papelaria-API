using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Exemplos;
using Crosscutting.Dtos.Auth.Login;
using Crosscutting.Dtos.Auth.Register;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(UserManager<Usuario> userManager, IConfiguration configuration)
        : ControllerBase
    {
        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT.
        /// </summary>
        /// <response code="200">Token JWT gerado com sucesso.</response>
        /// <response code="401">Usuário ou senha inválidos</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerRequestExample(typeof(LoginRequestDto), typeof(LoginRequestDtoExample))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, request.Senha))
                return Unauthorized("Usuário ou senha inválidos.");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        /// <summary>
        /// Realiza o registro de um novo usuário.
        /// </summary>
        /// <response code="200">Usuario registrado com sucesso</response>
        /// <response code="422">Requisição não atende as regras de validação</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [SwaggerRequestExample(typeof(RegistroRequestDto), typeof(RegistroRequestDtoExample))]
        public async Task<IActionResult> Register([FromBody] RegistroRequestDto request)
        {
            var user = new Usuario { UserName = request.NomeUsuario, NomeUsuario = request.NomeUsuario, Email = request.Email, NomeCompleto = request.NomeCompleto, DataCriacao = DateTime.Now};

            var result = await userManager.CreateAsync(user, request.Senha);

            if (!result.Succeeded)
                return UnprocessableEntity(new { Mensagens = result.Errors.Select(e=>e.Description).ToList() });

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

            var userRoles = await userManager.GetRolesAsync(user);
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtSecret = configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
                throw new InvalidOperationException("JWT Secret is not configured.");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:ValidIssuer"],
                audience: configuration["Jwt:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
