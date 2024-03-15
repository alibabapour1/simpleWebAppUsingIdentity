using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using simpleWebAppUsingIdentity.Models.Dtos;
using simpleWebAppUsingIdentity.Models.Entities;
using simpleWebAppUsingIdentity.Services;

namespace simpleWebAppUsingIdentity.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly Smtpconfiguration  _smtpconfiguration;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _smtpconfiguration = new Smtpconfiguration();
        }
        public IActionResult index()
        {
            return View();
        }

        public IActionResult RegisterUser() 
        {

            return View();
        }
        [HttpPost]
        public IActionResult RegisterUser(RegisterDto registerDto)
        {
            if(ModelState.IsValid==false) 
                return View(registerDto);


            User newuser = new User()
            {
               Email = registerDto.Email,
               FirstName = registerDto.FirstName,
               LastName = registerDto.LastName,
               UserName = registerDto.Email

            };

            var result = _userManager.CreateAsync(newuser,registerDto.Password).Result;

            if (result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(newuser).Result;
                string urlcallback = Url.Action("ConfirmEmail" , "User",new {token = code , UserId = newuser.Id,},protocol:Request.Scheme );
                string body = $"please click on the link below to validate your Emailaddress <a href={urlcallback} > Link </a>";

                _smtpconfiguration.Excute(newuser.Email, body, "Email Validation");

                RedirectToAction("DisplayEmail");
            }
            string message = "";
            foreach (var item in result.Errors) 
            {
                message = item.Description +Environment.NewLine;
            }
            TempData["error"] = message;
            return View(registerDto);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginDto { ReturnUrl = returnUrl });

        }
        [HttpPost]
        public IActionResult Login(LoginDto login)

        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userManager.FindByNameAsync(login.UserName).Result;

            if (user == null)
                ModelState.AddModelError(string.Empty, "User Not Found");

            _signInManager.SignOutAsync();
            var result = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent, true).Result;
            if (result.Succeeded)
            {
                return Redirect(login.ReturnUrl);
            }
            return View();
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DisplayEmail()
        {
            return View();
        }

        public IActionResult ConfirmEmail(string UserId,string Token)
        {
            if(UserId == null || Token==null) { return BadRequest(); }
            var user = _userManager.FindByIdAsync(UserId).Result;
            if (user == null)
                return BadRequest();
            var result = _userManager.ConfirmEmailAsync(user, Token).Result;
            if (result.Succeeded) { return View("Login"); }
            else { return BadRequest(); }

        }
        public IActionResult Forgetpassword() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forgetpassword(forgetpassword forget)
        {
            if(!ModelState.IsValid) { return View(forget); }

            var user = _userManager.FindByEmailAsync(forget.EmailAddress).Result;
            if (user == null || _userManager.IsEmailConfirmedAsync(user).Result == false)
            {
                ViewBag.Message = "The Emailaddress you entred may not exist or haven't been validated ";
                return View();

            }
            else 
            {
                string token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                string UrlCallBack = Url.Action("ResetPassword", "User", new { token=token,UserId=user.Id },Request.Scheme);
                ViewBag.Message = "An email containing the reset password link has benn sent to your email succesfully ! ";
                string body = $"Please click the link bleow to reset your password <a href={UrlCallBack}> Link </a> ";
                _smtpconfiguration.Excute(user.Email, body, "Reset Your Password");
                return View();
               
            }
        }

        public IActionResult ResetPassword(string token,string UserId) 
        {

            return View(new ResetPasswordDto {Token =token,UserId=UserId });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordDto reset)
        {
            if (!ModelState.IsValid) { return View(reset); }
            var user = _userManager.FindByIdAsync(reset.UserId).Result;
            if (user == null)
            { return BadRequest(); }
            var result = _userManager.ResetPasswordAsync(user, reset.Token, reset.Password).Result;
            if (result.Succeeded)
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            else
            {
                ViewBag.message = "The credential you entred is not valid";
                return View(reset);
            }


        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }
}
 