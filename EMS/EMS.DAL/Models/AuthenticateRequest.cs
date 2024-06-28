using System.ComponentModel;

namespace EMS.DAL.Models;

public class AuthenticateRequest
{
    [DefaultValue("anish@gmail.com")]
    public required string Email { get; set; }

    [DefaultValue("12345678")]
    public required string Password { get; set; }
}
