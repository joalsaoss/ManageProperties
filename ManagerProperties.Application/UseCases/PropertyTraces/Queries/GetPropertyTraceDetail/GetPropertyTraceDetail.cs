using ManagerProperties.Application.Utilities.Mediator;

namespace ManagerProperties.Application.UseCases.PropertyTraces.Queries.GetPropertyTraceDetail
{
    public class GetPropertyTraceDetail:IRequest<PropertyTraceDetailDTO>
    {
        public Guid Id { get; set; }
    }
}
