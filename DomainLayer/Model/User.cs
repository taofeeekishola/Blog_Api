using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } // Use PascalCase for consistency
        public string LastName { get; set; } // Use PascalCase for consistency
        //public DateOnly DateOfBirth { get; set; } // Use PascalCase for consistency
    }
}
