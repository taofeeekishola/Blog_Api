
namespace DomainLayer.UserDto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // This will be hashed by Identity
    }
}
