using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace JWTWebApiAuth.Services
{
    /// <summary>
    /// Class for operations results management
    /// </summary>
    public class ResultManagementService : IResultManagementService
    {
        public JsonResult Result(string message, string code)
        {
            return new JsonResult(
                new Dictionary<string, string>
                {
                    {"Message: ", message},
                    {"Code: ", code}
                }
            );
        }
    }
}