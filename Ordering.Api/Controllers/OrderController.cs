using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Commands.Checkout;
using Ordering.Application.Features.Queries.GetOrders;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]   
    public class OrderController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CheckoutOrderCommand command)
            => await mediator.Send(command);

        [AllowAnonymous]
        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<OrdersViewModel>>> GetOrders(string userName)
            => await mediator.Send(new GetOrdersListQuery { UserName = userName });
    }
}
