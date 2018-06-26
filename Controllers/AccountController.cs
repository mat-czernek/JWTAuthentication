using Microsoft.AspNetCore.Mvc;
using JWTWebApiAuth.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using JWTWebApiAuth.Infrastructure;
using JWTWebApiAuth.Services;

namespace JWTWebApiAuth.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserManagementService _userManagementService;

        private readonly ISignInManagementService _singInManagementService;

        private readonly JWTSettings _jwtSettings;

        private readonly IJwtManagement _jwtManagement;

        private readonly IResultManagementService _resultManagementService;

        
        public AccountController(IUserManagementService userManagementService, ISignInManagementService singInManagementService, 
                                    IOptions<JWTSettings> jwtSettings, IJwtManagement jwtManagement, IResultManagementService resultManagementService)
        {     
            _userManagementService = userManagementService;
            _singInManagementService = singInManagementService;
            _jwtManagement = jwtManagement;
            _jwtSettings = jwtSettings.Value;
            _resultManagementService = resultManagementService;
        }

        [HttpPost("register")]
        public async Task<object> Register([FromBody] UserCredentialsModel model)
        {
            var result = await _userManagementService.Create(model);

            if (result.Succeeded)
            {
                return _resultManagementService.Result("User registration success.", "200");
            }
            
            return _resultManagementService.Result("User registration failed.", "400");
        }


        [HttpPost("login")]
        public async Task<object> Login([FromBody] UserCredentialsModel credentials)
        {
            var result = await _singInManagementService.Login(credentials.Email, credentials.Password);

            if (result.Succeeded)
            {
                var appUser = _userManagementService.GetByEmail(credentials.Email);
                return _jwtManagement.GenerateJwtToken(credentials.Email, appUser.Result);
            }
            
            return _resultManagementService.Result("Invalid login attempt.", "400");
        }
    }
}