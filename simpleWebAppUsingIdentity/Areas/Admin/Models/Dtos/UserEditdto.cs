using System.ComponentModel.DataAnnotations;

namespace simpleWebAppUsingIdentity.Areas.Admin.Models.Dtos
{
    public class UserEditdto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

    }
}
