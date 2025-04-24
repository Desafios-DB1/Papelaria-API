using Crosscutting.Dtos.Auth.Login;
using Crosscutting.Dtos.Auth.Register;
using Domain.Entities;

namespace Domain.Mappers;

public static class UsuarioMapper
{
    public static RegisterRequestDto MapToRegisterRequestDto(Usuario usuario)
    {
        return new RegisterRequestDto
        {
            Email = usuario.Email,
            NomeCompleto = usuario.NomeCompleto,
            NomeUsuario = usuario.NomeUsuario,
            Senha = usuario.PasswordHash
        };
    }
    
    public static LoginRequestDto MapToLoginRequestDto(Usuario usuario)
    {
        return new LoginRequestDto
        {
            Email = usuario.Email,
            Senha = usuario.PasswordHash
        };
    }
}