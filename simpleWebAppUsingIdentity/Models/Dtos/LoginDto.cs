using System.ComponentModel.DataAnnotations;

namespace simpleWebAppUsingIdentity.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required]
        public bool IsPersistent { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}
