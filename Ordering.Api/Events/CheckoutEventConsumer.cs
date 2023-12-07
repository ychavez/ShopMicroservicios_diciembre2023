using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Commands.Checkout;

namespace Ordering.Api.Events
{
    public class CheckoutEventConsumer(IMapper mapper, IMediator mediator) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = mapper.Map<CheckoutOrderCommand>(context.Message);
            _ = await mediator.Send(command);
           
        }
    }
}
