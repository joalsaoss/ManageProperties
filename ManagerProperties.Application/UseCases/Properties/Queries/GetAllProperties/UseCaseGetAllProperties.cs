using ManagerProperties.Application.Contracts.Repositories;
using ManagerProperties.Application.Utilities.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerProperties.Application.UseCases.Properties.Queries.GetAllProperties
{
    public class UseCaseGetAllProperties : IRequestHandler<GetAllProperties, List<GetAllPropertiesDTO>>
    {
        private readonly IRepositoryProperties repository;

        public UseCaseGetAllProperties(IRepositoryProperties repository)
        {
            this.repository = repository;
        }
        public async Task<List<GetAllPropertiesDTO>> Handle(GetAllProperties request)
        {
            var properties = await repository.GetAll();
            var propertiesDTO = properties.Select(prope => prope.ToDTO()).ToList();
            return propertiesDTO;

        }
    }
}
