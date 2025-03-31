using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Services.Interfaces;

namespace RapidPay.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/fees")]
    [ApiVersion("1.0")]
    public class FeeController : ControllerBase
    {
        private readonly IFeeService _feeService;

        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        // GET api/fees
        /// <summary>
        /// Get the current transaction fee used in payments.
        /// </summary>
        /// <remarks>
        /// This fee is dynamically updated every hour by the Universal Fees Exchange (UFE).
        /// </remarks>
        /// <response code="200">Returns the current fee.</response>
        [HttpGet]
        [AllowAnonymous]
        public decimal GetCurrentFee()
        {
            var fee = _feeService.GetCurrentFee();
            return fee;
        }
    }
}
