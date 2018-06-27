using Microsoft.AspNetCore.Mvc;

namespace JWTWebApiAuth.Services
{
    /// <summary>
    /// Interface for operations results management
    /// </summary>
    public interface IResultManagementService
    {
        JsonResult Result(string message, string code);
    }
}