using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.UserWebClient.Models;

public class RegisterModel
{
    [Required]
    [RegularExpression(
        @"^(\+?[0-9]{11})$",
        ErrorMessage = "Неверный формат номера телефона")]
    public string PhoneNumber { get; set; }

    [Required]
    [RegularExpression(
        @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        ErrorMessage = "Неверный формат email")]
    public string Email { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string MiddleName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,128}$",
        ErrorMessage = "Пароль не удовлетворяет требованиям")]
    public string Password { get; set; }
}