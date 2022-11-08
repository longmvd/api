using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Misa.Amis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public string GetUserName()
        {
            return "{ name: Mai Đại Long}";
        }
    }
}
