using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Properties.Commands.DeleteProperty
{
    public class UseCaseDeleteProperty : IRequestHandler<CommandDeleteProperty>
    {
        private readonly IRepositoryProperties repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseDeleteProperty(IRepositoryProperties repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandDeleteProperty request)
        {
            var property = await repository.GetById(request.Id);

            if (property is null)
            {
                throw new NFoundException();
            }

            try
            {
                await repository.Delete(property);
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
