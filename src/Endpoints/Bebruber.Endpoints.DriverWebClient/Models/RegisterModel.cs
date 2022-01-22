using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.DriverWebClient.Models;

public class RegisterModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [RegularExpression(@"^(\+?[0-9]{11})$",
        ErrorMessage = "Неверный формат номера телефона")]
    public string PhoneNumber { get; set; }
    
    [Required]
    [RegularExpression(@"^([0-9]{2}\s?[0-9]{2}\s?[0-9]{6})$",
        ErrorMessage = "Неверный формат номера лицензии")]
    public string DriverLicense { get; set; }

    [Required]
    [RegularExpression(@"^[АВЕКМНОРСТУХ][0-9]{3}[АВЕКМНОРСТУХ]{2}\s?[0-9]{2,3}$",
        ErrorMessage = "Неверный формат автомобильного номера")]
    public string CarNumber { get; set; }

    [Required]
    public string CarType { get; set; }




}