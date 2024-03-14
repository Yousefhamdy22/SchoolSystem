using SchoolSystem.School.DAL.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.School.DAL.Data.Models.Admin
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password  { get; set; }

        //nav
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
