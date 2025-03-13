using AutoMapper;
using BusinessLogicLayer.IServices;
using DomainLayer.Model;
using DomainLayer.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Blog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IMapper _imapper;
        IAuthService _authService;

        public UserController(IUserService userService, IMapper imapper, IAuthService authService)
        {
            _userService = userService;
            _imapper = imapper;
            _authService = authService;
        }

        //endpoint to get all users
        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            // This method is synchronous
            return Ok(_imapper.Map<List<UserDto>>(_userService.GetAllUsers()));
        }

        //endpoint to get one user
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            // This method is asynchronous
            User? user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            UserDto existingUser = _imapper.Map<UserDto>(user);
            return Ok(existingUser);
        }

        //endpoint to create user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {

            // Call the service to create the user
            var createdUser = await _userService.CreateUser(userDto);

            if (createdUser == null)
            {
                return BadRequest("User creation failed."); // Return error message
            }

            // Map the created user to a DTO for the response
            var resultDto = _imapper.Map<UserDto>(createdUser);

            return Ok(resultDto); // Return the created user DTO
        }

        //endpoint to update a user
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userdto)
        {
            User existingUser = _imapper.Map<User>(userdto);
            // Assuming this is asynchronous, but modify if it's not
            User? userUpdate = await _userService.UpdateUser(existingUser);
            if (userUpdate is null)
            {
                return BadRequest();
            }

            UserDto newUser = _imapper.Map<UserDto>(userUpdate);
            return Ok(newUser);
        }

        //endpoint to delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Assuming this is asynchronous, but modify if it's not
            bool isDeleted = await _userService.DeleteUser(id);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Deleted successfully" });
        }

        // Endpoint for user login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            var user = await _userService.AuthenticateUser(loginDto);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            // Generate JWT token using the AuthService
            var token = _authService.GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                User = _imapper.Map<UserDto>(user)
            });
        }


    }
}