using System.Threading.Tasks;
using JWTWebApiAuth.Models;
using Microsoft.AspNetCore.Identity;

namespace JWTWebApiAuth.Services
{
    public interface IUserManagementService
    {
        Task<IdentityResult> Create(UserCredentialsModel model);

        Task<IdentityUser> GetByEmail(string email);
    }
    
}