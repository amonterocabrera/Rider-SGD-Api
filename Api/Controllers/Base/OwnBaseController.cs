using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SGDPEDIDOS.Api.Controllers.Base
{
    [Authorize]
    [ApiController]
    public class OwnBaseController : ControllerBase
    {

    }
}
