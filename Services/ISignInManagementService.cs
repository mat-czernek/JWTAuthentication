using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JWTWebApiAuth.Services
{
    /// <summary>
    /// Interface for user signin operations
    /// </summary>
    public interface ISignInManagementService
    {
        Task<SignInResult> Login(string email, string password);
    }
}