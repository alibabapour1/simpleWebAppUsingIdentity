using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using simpleWebAppUsingIdentity.Areas.Admin.Models.Dtos;
using simpleWebAppUsingIdentity.Models.Entities;
using System.Security.AccessControl;

namespace simpleWebAppUsingIdentity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Roles> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<Roles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.
                Select
                (p=> new UserListDto
                { 
                    Id=p.Id,
                    Email = p.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    EmailConfirmed = p.EmailConfirmed
                }
                
                )
                .ToList();
            return View(users);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(RegisterDto registerDto)
        {
            if (ModelState.IsValid == false)
                return View(registerDto);


            User newuser = new User()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email

            };

            var result = _userManager.CreateAsync(newuser, registerDto.Password).Result;

            if (result.Succeeded)
               return RedirectToAction("Index", "User", new {area = "Admin"});

            string message = "";
            foreach (var item in result.Errors)
            {
                message = item.Description + Environment.NewLine;
            }
            TempData["error"] = message;
            return View(registerDto);

        }

        public IActionResult Edit(string id) 
        {
            var users = _userManager.FindByIdAsync(id).Result;

            var EditedUsers = new UserEditdto { Id = users.Id, Email = users.Email, FirstName = users.FirstName, LastName = users.LastName, EmailConfirmed = users.EmailConfirmed };


            return View(EditedUsers);
        }

        [HttpPost]
        public IActionResult Edit(UserEditdto userEditdto)
        {
            var user = _userManager.FindByIdAsync(userEditdto.Id).Result;
            user.Email = userEditdto.Email;
            user.FirstName = userEditdto.FirstName;
            user.LastName = userEditdto.LastName;
            user.EmailConfirmed = userEditdto.EmailConfirmed;


            var result = _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            return    RedirectToAction("Index", "User", new { area = "Admin" });

            return View(userEditdto);
        }

        public IActionResult Delete(string id) 
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
                return View("Error");

            var DeleteUser = new UserDeleteDto { Id=user.Id,Email = user.Email,FirstName=user.FirstName,LastName = user.LastName };
            return View(DeleteUser);
        }

        [HttpPost]
        public IActionResult Delete(UserDeleteDto userdeletedto)
        {
            var user = _userManager.FindByIdAsync(userdeletedto.Id).Result;
            var resutlt= _userManager.DeleteAsync(user).Result;
            if (resutlt.Succeeded) { return RedirectToAction("Index", "User", new { area = "Admin" });};
            return View();
        }
        public IActionResult AddUserRole(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var roles = new List<SelectListItem>
                ( _roleManager.Roles.Select(p => new SelectListItem { Text = p.Name, Value = p.Name })
                ).ToList();
            return View(new AddUserRoleDto { Id = id, Roles = roles, Fullname = $"{user.FirstName}   {user.LastName}", UserName = user.UserName });
        }
        [HttpPost]
        public IActionResult AddUserRole(AddUserRoleDto userRoleDto)
        {
            var user = _userManager.FindByIdAsync(userRoleDto.Id).Result;
            _userManager.AddToRoleAsync(user, userRoleDto.Role);
            return RedirectToAction("UserRoles", "User", new { Id = user.Id, area = "Admin" });
        }
        public IActionResult UserRoles(string Id) 
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            var role = _userManager.GetRolesAsync(user).Result;
            ViewBag.Name = $"{user.FirstName}  {user.LastName}  Email : {user.UserName}";
            return View(role);
        }
    }
} 
