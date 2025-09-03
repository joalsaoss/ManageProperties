using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails
{
    public class GetOwnerDetail: IRequest<OwnerDetailDTO>
    {
        public Guid Id { get; set; }
    }
}
