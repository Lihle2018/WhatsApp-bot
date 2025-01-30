
using Assessments.Application.DTOs;
using MediatR;

namespace Assessments.Application.UseCases.GetQuestions
{
    public record GetQuestionsQuery : IRequest<IEnumerable<QuestionDto>>;
}
