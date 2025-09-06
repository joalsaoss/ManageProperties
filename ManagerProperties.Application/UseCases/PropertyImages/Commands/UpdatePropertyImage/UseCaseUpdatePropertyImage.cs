using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Exceptions;
using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.PropertyImages.Commands.UpdatePropertyImage
{
    public class UseCaseUpdatePropertyImage : IRequestHandler<CommandUpdatePropertyImage>
    {
        private readonly IRepositoryPropertyImages repository;
        private readonly IUnitOfWork unitOfWork;

        public UseCaseUpdatePropertyImage(IRepositoryPropertyImages repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(CommandUpdatePropertyImage request)
        {
            var propertyImage = await repository.GetById(request.Id);
            
            if (propertyImage is null)
            {
                throw new NFoundException();
            }

            propertyImage.Update(request.PropertyId, request.Image, request.Enable);
            
            try
            {
                await repository.Update(propertyImage);
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
