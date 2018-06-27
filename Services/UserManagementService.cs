using System.Linq;
using System.Threading.Tasks;
using JWTWebApiAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWTWebApiAuth.Services
{
    /// <summary>
    /// Class for user operations management
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManagementService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Create(UserCredentialsModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email, 
                Email = model.Email
            };

            return await _userManager.CreateAsync(user, model.Password);
        }


        public async Task<IdentityUser> GetByEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
                return null;

            return await _userManager.Users.SingleOrDefaultAsync(usr => usr.Email == email);
        }
    }
}