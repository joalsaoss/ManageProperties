using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ManageProperties.Tests.Application.UseCases.Owners
{
    [TestFixture]
    public class GetOwnerDetailTest
    {
        private IRepositoryOwners repositoryOwners;
        private UseCaseGetOwnerDetail useCase;

        [SetUp]
        public void SetUp()
        {
            repositoryOwners = Substitute.For<IRepositoryOwners>();
            useCase = new UseCaseGetOwnerDetail(repositoryOwners);
        }

        [Test]
        public async Task Handle_Create_Owner_And_Persist()
        {
            //var address = new Address("Address")
            var owner = new Owner("Nombre Owner", "Address", [],DateTime.Parse("2000-07-23"));
            var id = owner.Id;
            var query = new GetOwnerDetail { Id = id };

            repositoryOwners.GetById(id).Returns(owner);

            var result = await useCase.Handle(query);

            Assert.That(result, Is.Not.Null);
            Assert.That(id, Is.EqualTo(result.Id));
            Assert.That(owner.Name, Is.EqualTo(result.Name));

        }

        [Test]
        public void Handle_Owner_Not_Exists_Throw_Exception()
        {
            var id = Guid.NewGuid();
            var query = new GetOwnerDetail { Id = id};

            repositoryOwners.GetById(id).ReturnsNull();

            // Act
            var ex = Assert.ThrowsAsync<NFoundException>(() =>  useCase.Handle(query));

            // Assert|
            Assert.That(ex, Is.Not.Null);

        }
    }
}
