using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.UseCases.Owners.Commands.CreateOwner;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace ManageProperties.Tests.Application.UseCases.Owners
{
    [TestFixture]
    public class CreateOwnerTest
    {
        private IRepositoryOwners _repo = null!;
        private IUnitOfWork _uow = null!;
        private UseCaseCreateOwner _sut = null!;

        [SetUp]
        public void SetUp()
        {
            _repo = Substitute.For<IRepositoryOwners>();
            _uow = Substitute.For<IUnitOfWork>();
            _sut = new UseCaseCreateOwner(_repo, _uow);
        }

        private static CommandCreateOwner MakeCommand(
            string name = "Juan Pérez",
            string photo = "photo.png",
            string birthday = "1990-01-01")
        {
            // Ajusta esta construcción según tu definición real de CommandCreateOwner
            // (si es record posicional, usa el ctor; si es clase, setea propiedades).
            var address = "Cra 123 #45-67, Medellín";

            // Variante típica como clase con props settable:
            return new CommandCreateOwner
            {
                Name = name,
                Address = address,
                Birthday = DateTime.Parse(birthday), 
                Bytes = Stream.Null, 
                ContentType = "image/png", 
                PhotoFileName= "photo.png"
            };

            // Si fuera record posicional, usa:
            // return new CommandCreateOwner(name, address, photo, birthday);
        }

        [Test]
        public async Task Handle_Create_Owner_And_Persist()
        {
            var command = new CommandCreateOwner{ 
                Address = "Address", 
                Name = "Nombre Owner", 
                Birthday = DateTime.Parse("2000-07-23"), 
                Bytes = Stream.Null, 
                ContentType = "image/png",
                PhotoFileName = "photo.png"
            };

            var ownerCreated = new Owner("Nombre Owner", "Address", [], DateTime.Parse("2000-07-23"));

            _repo.Create(Arg.Any<Owner>()).Returns(ownerCreated);
            var result = await _sut.Handle(command);
            
            await _repo.Received(1).Create(Arg.Any<Owner>());
            await _uow.Received(1).Persist();
            Assert.That(result, Is.Not.EqualTo(Guid.Empty));

        }

        [Test]
        public void Handle_Command_Valid_Throw_Exception_Rollback()
        {
            // Arrange
            var cmd = MakeCommand();

            _repo.Create(Arg.Any<Owner>()).ThrowsAsync<Exception>();

            // Act
            var ex = Assert.ThrowsAsync<Exception>(() => _sut.Handle(cmd));

            // Assert
            Assert.That(ex, Is.Not.Null);

            // No side effects
            _uow.DidNotReceive().Persist();
            _uow.Received(1).RollBack();

        }



    }
}
