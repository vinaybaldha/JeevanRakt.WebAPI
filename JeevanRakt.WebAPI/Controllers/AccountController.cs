using JeevanRakt.Core.Domain.Identity;
using JeevanRakt.Core.DTO;
using JeevanRakt.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;


        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        /// <summary>
        /// this method is used to register user
        /// </summary>
        /// <param name="registerDTO">get register data from user</param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return NotFound();//http 404
            } 

            //Validation
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join("|", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

                return Problem(errorMessage);
            }


            //create user
            ApplicationUser user = new ApplicationUser()
            {
                EmployeeName = registerDTO.EmployeeName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
                

            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Join("|", result.Errors.Select(x => x.Description));

                return Problem(errorMessage);
            }

            return Ok(registerDTO);

        }

        /// <summary>
        /// this action check if email is already available in the user database or not
        /// </summary>
        /// <param name="email">email to verify in database</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegister(string email)
        {
            if (email == null)
            {
                return NotFound();//http 404
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        /// <summary>
        /// this action method login user based of email and password
        /// </summary>
        /// <param name="loginDTO">model for login action method</param>
        /// <returns>user name and email</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
        {
            //Validation
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)));

                return Problem(errorMessage);
            }

            //login
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Problem("Email and Password is not valid");
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return NoContent(); //http 204
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                return BadRequest("something went wrong");
            }

            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user, roles.ToList());

            return Ok(authenticationResponse);

        }

        /// <summary>
        /// this action method logout current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        //[HttpPost("forgot-password")]
        //[AllowAnonymous]

        //public async Task<IActionResult> ForgotPassword([FromForm][Required] string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user == null) { return BadRequest("email is not registered"); }

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    var forgotPasswordLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

        //    if (forgotPasswordLink == null) { return BadRequest("forgot password link is not generated"); }

        //    await _emailSender.SendEmailAsync(email, "Forgot Password Link", forgotPasswordLink);

        //    return Ok($"Password reset link is send to your email {email}");


        //}

        //[HttpGet("reset-password")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(string token, string email)
        //{
        //    var model = new ResetPasswordDTO { Token = token, Email = email };

        //    return Ok(model);
        //}


        //[HttpPost("reset-password")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        //{
        //    if (resetPasswordDTO == null || resetPasswordDTO.Email == null) { return BadRequest(); }

        //    var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);

        //    if (user == null) { BadRequest("user is not valid"); }

        //    var resetPassword = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.Password);

        //    if (!resetPassword.Succeeded)
        //    {
        //        foreach (var error in resetPassword.Errors)
        //        {
        //            ModelState.AddModelError(error.Code, error.Description);
        //        }
        //        return BadRequest(ModelState);
        //    }

        //    return Ok("password has been changed");

        //}

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public IActionResult AllUser()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpGet("totalusers")]
        public async Task<ActionResult<int>> GetTotalUsersCount()
        {
            var totalUsersCount = await _userManager.Users.CountAsync();
            return totalUsersCount;
        }

        [HttpGet("getroles/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            // Get the user by userId
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                // User not found
                return NotFound();
            }

            // Get the roles assigned to the user
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

    }
}
