namespace SchoolSystem.DTOs.Identity
{
    public record RegisterUserDto
    (
        string AppUserId,
        string firstName,
        string lastName,
        string Address,

        AppuserDto AppUser
    );
}
