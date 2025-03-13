using Microsoft.AspNetCore.Identity;


namespace DomainLayer.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } // Use PascalCase for consistency
        public string LastName { get; set; } // Use PascalCase for consistency
        //public DateOnly DateOfBirth { get; set; } // Use PascalCase for consistency
    }
}
