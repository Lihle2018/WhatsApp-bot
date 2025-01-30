using Assessments.Application.DTOs;
using MediatR;

namespace Assessments.Application.UseCases.GenerateQuestions
{
    public record GenerateQuestionsCommand(string Trait, string Topic, int Difficulty, int Count) : IRequest<IEnumerable<QuestionDto>>;
}
