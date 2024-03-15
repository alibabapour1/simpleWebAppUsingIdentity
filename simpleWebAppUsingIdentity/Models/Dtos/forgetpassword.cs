using System.ComponentModel.DataAnnotations;

namespace simpleWebAppUsingIdentity.Models.Dtos
{
    public class forgetpassword
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
