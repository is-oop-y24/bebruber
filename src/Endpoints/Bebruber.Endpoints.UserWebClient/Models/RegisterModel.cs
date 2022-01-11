using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.UserWebClient.Models;

public class RegisterModel
{
    [Required]
    [RegularExpression(@"^(\+[0-9]{11})$", 
        ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,128}$",
        ErrorMessage = "Password must contain minimum 8 and maximum 128 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
    public string Password { get; set; }
}