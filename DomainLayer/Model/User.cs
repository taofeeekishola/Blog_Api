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
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateOnly DateofBirth { get; set; }
    }
}
