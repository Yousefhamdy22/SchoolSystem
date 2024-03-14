using SchoolSystem.School.DAL.Data.Models.Admin;

namespace SchoolSystem.School.DAL.Data.Identity
{
    public interface IAppUser
    {
        Admin? Admin { get; set; }
        string? PhoneNumber { get; set; }
    }
}