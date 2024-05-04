using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.Identity;
using JeevanRakt.Core.Domain.RepositoryContracts;
using JeevanRakt.Core.DTO;
using JeevanRakt.Core.ServiceContracts;
using JeevanRakt.Infrastructure.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JeevanRakt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IEmailSender _emailSender;
        private readonly IImageRepository _imageRepository;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService, IEmailSender emailSender, IImageRepository imageRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _emailSender = emailSender;
            _imageRepository = imageRepository;
            _context = context;
            _roleManager = roleManager;
        }

        /// <summary>
        /// this method is used to register user
        /// </summary>
        /// <param name="registerDTO">get register data from user</param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        //[Authorize(Roles ="Admin")]
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

            IdentityResult result2 = await _userManager.AddToRoleAsync(user, "User");

            if (!result2.Succeeded) { return NotFound(); }

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

            authenticationResponse.FilePath = user.FilePath;
            authenticationResponse.Role = roles[0];
            if(user.BloodBankId != null)
            {
                authenticationResponse.BloodBankId = (Guid)user.BloodBankId;
            }
            
          

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

        [HttpPost("forgot-password")]
        [AllowAnonymous]

        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            string? email = request.Email;

            if(email == null) { return NotFound("email is not found"); }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) { return BadRequest("email is not registered"); }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var forgotPasswordLink = $"http://localhost:4200/reset-password?token={token}&email={email}";

            if (forgotPasswordLink == null) { return BadRequest("forgot password link is not generated"); }

            await _emailSender.SendEmailAsync(email, "Forgot Password Link", forgotPasswordLink);

            return Ok($"Password reset link is send to your email {email}");


        }

        [HttpGet("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDTO { Token = token, Email = email };

            return Ok(model);
        }


        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (resetPasswordDTO == null || resetPasswordDTO.Email == null) { return BadRequest(); }

            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);

            if (user == null) { BadRequest("user is not valid"); }
            resetPasswordDTO.Token = resetPasswordDTO.Token.Replace(" ", "+");
            var resetPassword = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.Password);

            if (!resetPassword.Succeeded)
            {
                foreach (var error in resetPassword.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok("password has been changed");

        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public IActionResult AllUser()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();

            // Create a list to store user information along with their roles
            //List<UserResponseDTO> usersWithRoles = new List<UserResponseDTO>();

            //// Iterate through each user to get their roles
            //foreach (var user in users)
            //{
            //    // Get roles for the user
            //    IList<string> roles = _userManager.GetRolesAsync(user).Result;

            //    // Create a DTO object to hold user information and roles
            //    var userWithRoles = new UserResponseDTO
            //    {
                    
            //        EmployeeName = user.EmployeeName,
            //        Email = user.Email,
            //        PhoneNumber = user.PhoneNumber,
            //        Role = roles
            //    };

            //    // Add the DTO object to the list
            //    usersWithRoles.Add(userWithRoles);
            //}

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

        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm]ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);


            if (ModelState.IsValid)
            {
               
                //Convert DTO to model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                };


                //user repository to upload image
                await _imageRepository.Upload(imageDomainModel);

                ApplicationUser? user = await _userManager.GetUserAsync(User);

                if(user == null) { return  NotFound("user not found"); }

                user.FilePath = imageDomainModel.FilePath;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Ok(new { imageDomainModel.FilePath });
                }
                else
                {
                    return BadRequest("Failed to update user profile.");
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size is more than 10MB, please upload a smaller size file");
            }
        }

        [HttpPut("user")]
        [Authorize]
        public async Task<ActionResult<int>> UpdateUser(ApplicationUser updatedUser)
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if(user == null) { return NotFound("user not found"); }

            user.EmployeeName = updatedUser.EmployeeName;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Email = updatedUser.Email;

            await _userManager.UpdateAsync(user);

            return Ok(user);
        }


        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<int>> GetUser()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if (user == null) { return NotFound("user not found"); }

            return Ok(user);
        }

        [HttpGet("user/valid")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> IsUserDuplicate([FromQuery] string username)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            if (user == null) { return Ok(false); }

            return Ok(true);
        }

        [HttpGet("user/roleaccess")]
        [AllowAnonymous]
        public async Task<ActionResult> RoleAccessMenus([FromQuery] string userrole, string? menu)
       {
            if (menu == null)
            {
                List<RoleAccess> menus = await _context.RoleAccesses.Where(x => x.Role == userrole).ToListAsync();

                return Ok(menus);
            }

            List<RoleAccess> menus1 = await _context.RoleAccesses.Where(x=>x.Role == userrole && x.Menu == menu).ToListAsync();  

            return Ok(menus1);
            
        }

    }
}
