using Microsoft.AspNetCore.Identity;
using SchoolSystem.School.DAL.Data.Models.AuthJWT;

namespace SchoolSystem.Services
{
    public interface IAuthoServices
    {
        Task<AuthoModel> RegisterAsync(Registration model);
        Task<AuthoModel> GetTokenAsync(TokenReqmodel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task GenerateJwtToken(IdentityUser user);
    }
}
