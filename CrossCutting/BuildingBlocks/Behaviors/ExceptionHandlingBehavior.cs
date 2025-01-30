using BuildingBlocks.BaseEntities;
using MediatR;
using Serilog;

namespace BuildingBlocks.Behaviors
{
    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An exception occurred when handling {RequestName}", typeof(TRequest).Name);

                // Create a failure response
                var response = new ResponseBase<TResponse>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                };

                // Optionally, add more specific error messages
                response.AddError(ex.Message);

                // Cast the response to TResponse before returning
                return (TResponse)(object)response;
            }
        }
    }

}