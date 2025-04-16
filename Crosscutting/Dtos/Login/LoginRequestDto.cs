using System;

namespace Crosscutting.Dtos.Login;

public class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
