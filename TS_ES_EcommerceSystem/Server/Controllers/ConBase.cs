using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class ConBase : ControllerBase
    {
    }

}
