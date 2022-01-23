using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.DriverWebClient.Models;

public class LoginModel
{
    [Required]
    [RegularExpression(
        @"^(\+?[0-9]{11})$",
        ErrorMessage = "Неверный формат номера телефона")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }
}