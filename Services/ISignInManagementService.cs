using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JWTWebApiAuth.Services
{
    public interface ISignInManagementService
    {
        Task<SignInResult> Login(string email, string password);
    }
}