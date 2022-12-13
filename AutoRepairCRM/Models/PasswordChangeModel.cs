using System.ComponentModel.DataAnnotations;

namespace AutoRepairCRM.Models;

public class PasswordChangeModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}