using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.DriverWebClient.Models;

public class RegisterModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string MiddleName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [RegularExpression(
        @"^(\+?[0-9]{11})$",
        ErrorMessage = "Неверный формат номера телефона")]
    public string PhoneNumber { get; set; }

    // [Required]
    // [RegularExpression(
    //     @"^([0-9]{2}\s?[0-9]{2}\s?[0-9]{6})$",
    //     ErrorMessage = "Неверный формат номера лицензии")]
    // public string DriverLicense { get; set; }
    [Required]
    [RegularExpression(
        @"^[АВЕКМНОРСТУХ][0-9]{3}[АВЕКМНОРСТУХ]{2}\s?[0-9]{2,3}$",
        ErrorMessage = "Неверный формат автомобильного номера")]
    public string CarNumber { get; set; }

    [Required]
    public string CarCategory { get; set; }

    [Required]
    public string CarBrand { get; set; }

    [Required]
    public string CarName { get; set; }

    [Required]
    public string CarColor { get; set; }

    [Required]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,128}$",
        ErrorMessage = "Пароль не удовлетворяет требованиям")]
    public string Password { get; set; }
}