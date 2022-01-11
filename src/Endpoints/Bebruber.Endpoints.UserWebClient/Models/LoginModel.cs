using System.ComponentModel.DataAnnotations;

namespace Bebruber.Endpoints.UserWebClient.Models;

public class LoginModel
{
    [Required]
    [RegularExpression(@"^(\+[0-9]{11})$", 
        ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string Password { get; set; }
}