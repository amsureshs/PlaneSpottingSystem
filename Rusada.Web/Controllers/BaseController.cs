using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rusada.Web.Filters;
using System.Security.Claims;

namespace Rusada.Web.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        public string UserId
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
    }
}

