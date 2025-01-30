using Carter;
using MediatR;
using Accounts.Application.UseCases.RegisterAccount;
using BuildingBlocks.BaseEntities;

namespace Accounts.API.Endpoints.Accounts
{
    public class Register : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/accounts/register", async (RegisterAccountRequest request, ISender sender) =>
            {
                try
                {
                    var response = await sender.Send(new RegisterAccountCommand(request.Email, request.Password));
                    return Results.Created($"/accounts/{response.Data.AccountId}", response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(new ResponseBase() { Success = false, Message = "Something went wrong", Errors = new List<string>() { e.Message } });
                }
            });
        }
    }
}