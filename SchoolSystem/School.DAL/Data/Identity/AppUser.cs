using Microsoft.AspNetCore.Identity;
using SchoolSystem.School.DAL.Data.Models.Admin;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.School.DAL.Data.Identity
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
   
        //
        public Admin? Admin { get; set; }


    }
}
