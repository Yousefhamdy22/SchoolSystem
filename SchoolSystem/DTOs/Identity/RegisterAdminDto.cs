namespace SchoolSystem.DTOs.Identity
{
    public class RegisterAdminDto
    {
        public string AppUserId { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AppuserDto AppUser { get; set; }

    }
}
