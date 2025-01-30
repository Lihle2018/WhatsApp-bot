using Assessments.Application.UseCases.GetQuestions;
using BuildingBlocks.BaseEntities;
using Carter;
using MediatR;

namespace Assessments.API.Endpoints
{
    public class GetQuestions: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/questions", async (ISender sender) =>
            {
                try
                {
                    var response = await sender.Send(new GetQuestionsQuery());
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(new ResponseBase() { Success = false, Message = "Something went wrong", Errors = new List<string>() { e.Message } });
                }
            });
        }
    }
}
