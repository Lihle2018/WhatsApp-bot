using Assessments.Application.UseCases.GenerateQuestions;
using BuildingBlocks.BaseEntities;
using Carter;
using MediatR;

namespace Assessments.API.Endpoints
{
    public class GenerateQuestion:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/questions/generate", async (GenerateQuestionRequest request, ISender sender) =>
            {
                try
                {
                    var response = await sender.Send(new GenerateQuestionsCommand(request.Trait,request.Topic,request.Difficulty,request.Count));
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
