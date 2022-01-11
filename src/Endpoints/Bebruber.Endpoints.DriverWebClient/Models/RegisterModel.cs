using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.DriverWebClient.Models;

public class RegisterModel
{
    [Required]
    public string RegistrationToken { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,128}$",
        ErrorMessage = "Password must contain minimum 8 and maximum 128 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
    public string Password { get; set; }
}