using System.ComponentModel.DataAnnotations;

namespace TD.CongDan.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}