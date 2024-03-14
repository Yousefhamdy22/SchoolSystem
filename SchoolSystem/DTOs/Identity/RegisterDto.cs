namespace SchoolSystem.DTOs.Identity
{
    public record RegisterDto(
        string Email,
        string Password,
        string phone,
        string firstName,
        string lastName,
        string Address,
        string userName
        );
}
