using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.Shared.Models;

public class LoginModel
{
    [Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "Неверный адрес почты")]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}