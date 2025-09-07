using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyImages.Queries.GetPropertyImageDetail
{
    public class GetPropertyImageDetail: IRequest<PropertyImageDetailDTO>
    {
        public required Guid Id { get; set; }
    }
}
