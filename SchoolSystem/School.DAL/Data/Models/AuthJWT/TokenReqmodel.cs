using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.School.DAL.Data.Models.AuthJWT
{
    public class TokenReqmodel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
