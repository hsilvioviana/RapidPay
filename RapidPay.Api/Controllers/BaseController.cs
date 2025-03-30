using Microsoft.AspNetCore.Mvc;

namespace RapidPay.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected Guid UserId => Guid.Parse(User.FindFirst("id")?.Value);
    }
}
