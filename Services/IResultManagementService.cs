using Microsoft.AspNetCore.Mvc;

namespace JWTWebApiAuth.Services
{
    public interface IResultManagementService
    {
        JsonResult Result(string message, string code);
    }
}