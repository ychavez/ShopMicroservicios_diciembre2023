using AutoMapper;
using Moq;
using Ordering.Application.Contracts;
using Ordering.Application.Features.Commands.Checkout;
using Ordering.Domain.Entities;

namespace Ordering.Test.Features.Orders.Checkout
{
    public class CheckoutOrderCommandHandlerTest
    {

        private readonly Mock<IGenericRepository<Order>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CheckoutOrderCommandHandler _hanlder;



        public CheckoutOrderCommandHandlerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IGenericRepository<Order>>();
            _hanlder = new CheckoutOrderCommandHandler(_repositoryMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task Handle_ShouldAddNewOrderToRepository()
        {
            //Arrange
            var command = new CheckoutOrderCommand()
            {
                UserName = "testUser",
                Address = "hola@hola.com",
                FirstName = "first",
                LastName = "last",
                PaymentMethod = 1,
                TotalPrice = 10
            };

            var orderEntity = new Order { Id = 12 };
            _mapperMock.Setup(x => x.Map<Order>(command)).Returns(orderEntity);
            _repositoryMock.Setup(x => x.AddAsync(orderEntity)).ReturnsAsync(orderEntity);

            //Act
            var result = await _hanlder.Handle(command, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.AddAsync(orderEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<Order>(command), Times.Once);
            Assert.Equal(result, orderEntity.Id);

        }



        [Theory]
        [InlineData(1,1,2)]
        [InlineData(1, 2, 3)]
        [InlineData(1, 3, 4)]
        [InlineData(1, 4, 5)]
        [InlineData(1, 5, 6)]
        public async Task OnePlusOne(int value1, int value2, int expected) 
        {
            //Arrange 
            int a = value1;
            int b = value2;

            //Act
            var result = new Calculator().Sum(a, b);


            //Assert
            Assert.Equal(result, expected);
        }
    }

    public class Calculator()
    {
        public int Sum(int a, int b)
        {
            if (b == 5)
            {
                b++;
            }
            return a + b;
        }
    }
}
