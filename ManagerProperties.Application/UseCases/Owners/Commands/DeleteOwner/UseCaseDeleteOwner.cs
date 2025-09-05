using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Owners.Commands.DeleteOwner
{
    public class UseCaseDeleteOwner : IRequestHandler<CommandDeleteOwner>
    {
        private readonly IRepositoryOwners repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseDeleteOwner(IRepositoryOwners repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandDeleteOwner request)
        {
            var owner = await repository.GetById(request.Id);

            if (owner is null)
            {
                throw new NFoundException();
            }

            try
            {
                await repository.Delete(owner);
                await unitOfWork.Persist();
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }

        }
    }
}
