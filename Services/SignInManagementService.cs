using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JWTWebApiAuth.Services
{
    public class SignInManagementService : ISignInManagementService
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SignInManagementService(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(email, password, false, false); 
        }
    }
}