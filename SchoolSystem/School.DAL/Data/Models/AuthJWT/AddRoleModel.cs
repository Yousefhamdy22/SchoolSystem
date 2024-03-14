using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.School.DAL.Data.Models.AuthJWT
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
