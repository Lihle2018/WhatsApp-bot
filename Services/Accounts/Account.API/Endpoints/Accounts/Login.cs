using Accounts.Application.UseCases.Login;
using BuildingBlocks.BaseEntities;
using Carter;
using MediatR;

namespace Accounts.API.Endpoints.Accounts
{
    public class Login : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginRequest request, ISender sender) =>
            {
                try
                {
                    var response = await sender.Send(new LoginCommand(request.Email, request.Password));
                    return response.Success?Results.Ok(response):Results.BadRequest(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(new ResponseBase() { Success=false,Message="Something went wrong",Errors =new List<string>() { e.Message} });
                }
            });
        }
    }
}
