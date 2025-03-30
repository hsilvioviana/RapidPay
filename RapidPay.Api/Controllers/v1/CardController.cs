using Microsoft.AspNetCore.Mvc;
using RapidPay.Services.Interfaces;

namespace RapidPay.Api.Controllers.v1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<bool> Get()
        {
            return await Task.FromResult(true);
        }
    }
}
