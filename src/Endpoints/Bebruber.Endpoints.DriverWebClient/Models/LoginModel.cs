using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.DriverWebClient.Models;

public class LoginModel
{
    [Required]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Неверный формат номера телефона")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }
}