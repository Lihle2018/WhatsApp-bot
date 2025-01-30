using MediatR;
using BuildingBlocks.BaseEntities;
namespace BuildingBlocks.Behaviors
{
    public class AuditingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (response is BaseEntity entity)
            {
                if (entity.DateCreated == default)
                {
                    entity.SetDateCreated();
                }
                entity.UpdateModificationDate();
            }

            return response;
        }
    }
}
