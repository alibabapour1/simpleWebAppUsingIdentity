using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using simpleWebAppUsingIdentity.Areas.Admin.Models.Dtos;
using simpleWebAppUsingIdentity.Models.Entities;

namespace simpleWebAppUsingIdentity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
       
        private readonly RoleManager<Roles> _roleManager;
        public RolesController(RoleManager<Roles>  roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {

            var roles = _roleManager.Roles.Select(p => new ListRolesDto { Id = p.Id, Description = p.Description, Name = p.Name }).ToList();


            return View(roles);


        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }



        [HttpPost]
        public IActionResult Create(CreateRolesDto createRoles)
        { 

            Roles newrole = new Roles { Name= createRoles.Name,Description=createRoles.Description };
            var result = _roleManager.CreateAsync(newrole).Result;
            if(result.Succeeded)
                return RedirectToAction("Index","Roles", new {area = "Admin"});
            else
              ViewBag.Errors =result.Errors.ToList();

            return View(createRoles);
        }

        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var result = _roleManager.FindByIdAsync(id).Result;
            var role = new EditRolesDto() { Description = result.Description, Name = result.Name };
            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(EditRolesDto editRoles)
        {
            var role = _roleManager.FindByIdAsync(editRoles.Id).Result;
            role.Description = editRoles.Description;
            role.Name = editRoles.Name;

            var result = _roleManager.UpdateAsync(role).Result;
            if (result.Succeeded)
                return RedirectToAction("Index", "Roles", new { area = "Admin" });



            else
                return View(role);

        }

        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null)
                return View("Error");

            var DeleteRole = new DeleteRoleDto { Id = role.Id, Name = role.Name, Description = role.Description };
            return View(DeleteRole);
        }

        [HttpPost]
        public IActionResult Delete(DeleteRoleDto deleteRoleDto )
        {
            var role = _roleManager.FindByIdAsync(deleteRoleDto.Id).Result;
            var resutlt = _roleManager.DeleteAsync(role).Result;
            if (resutlt.Succeeded) { return RedirectToAction("Index", "User", new { area = "Admin" }); };
            return View();
        }





    }
}
