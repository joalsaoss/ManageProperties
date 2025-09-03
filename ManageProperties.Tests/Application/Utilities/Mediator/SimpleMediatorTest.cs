using FluentValidation;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;
using NSubstitute;

namespace ManageProperties.Tests.Application.Utilities.Mediator
{
    [TestFixture]
    public class SimpleMediatorTest
    {
        public class FakeRequest : IRequest<string> {
            public required string Name { get; set; }
        }
        public class FakeHandle : IRequestHandler<FakeRequest, string> {
            public Task<string> Handle(FakeRequest request)
            {
                return Task.FromResult("Respuesta correcta");
            }
        }

        public class FakeValidatorRequest : AbstractValidator<FakeRequest>
        {
            public FakeValidatorRequest()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        [Test]
        public async Task Send_Called_Handle_Method()
        {
            var request = new FakeRequest() { Name = "Nombre A" };

            var useCaseMock = Substitute.For<IRequestHandler<FakeRequest, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.GetService(typeof(IRequestHandler<FakeRequest, string>)).Returns(useCaseMock);

            var mediator = new SimpleMediator(serviceProvider);

            var result = await mediator.Send(request);
            await useCaseMock.Received(1).Handle(request);
        }

        [Test]

        public void Send_Without_Handle_Registered_Throw_Exception()
        {
            var request = new FakeRequest() { Name = "Nombre A" };

            var useCaseMock = Substitute.For<IRequestHandler<FakeRequest, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();

            var mediator = new SimpleMediator(serviceProvider);

            var ex = Assert.ThrowsAsync<MediatorException>(() => mediator.Send<string>(request));
        }

        [Test]

        public void Send_Invalid_Command_Throw_Exception()
        {
            var request = new FakeRequest() { Name = " " };
            var serviceProvider = Substitute.For<IServiceProvider>();
            var validator = new FakeValidatorRequest();

            serviceProvider.GetService(typeof(IValidator<FakeRequest>)).Returns(validator);

            var mediator = new SimpleMediator(serviceProvider);

            Assert.ThrowsAsync<ValidException>(async () =>
            {
                await mediator.Send(request);
            });
        }
    }
}
