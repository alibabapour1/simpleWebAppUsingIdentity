using System.ComponentModel.DataAnnotations;

namespace simpleWebAppUsingIdentity.Models.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
