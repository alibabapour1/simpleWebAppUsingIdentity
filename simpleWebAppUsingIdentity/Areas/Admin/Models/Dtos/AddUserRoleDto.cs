using Microsoft.AspNetCore.Mvc.Rendering;

namespace simpleWebAppUsingIdentity.Areas.Admin.Models.Dtos
{
    public class AddUserRoleDto
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public string Fullname { get; set; }
        public string UserName  { get; set; }

    }
}
